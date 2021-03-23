using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Linq;
using Riven.Authorization;

namespace Riven.Identity.Permissions
{
    /// <summary>
    /// 权限初始化器
    /// </summary>
    public interface IPermissionInitializer
    {
        /// <summary>
        /// 运行
        /// </summary>
        IEnumerable<PermissionItem> Run();

    }

    public abstract class PermissionInitializer : IPermissionInitializer
    {
        public virtual Type PermissionAuthorizeAttributeType { get; protected set; }

        public PermissionInitializer()
        {
            PermissionAuthorizeAttributeType = typeof(IPermissionAuthorizeAttribute);
        }

        public virtual IEnumerable<PermissionItem> Run()
        {
            var attributes = this.GetPermissionAuthorizeAttributes();
            return AttrToItems(attributes);
        }


        protected abstract IEnumerable<IPermissionAuthorizeAttribute> GetPermissionAuthorizeAttributes();

        protected virtual IEnumerable<IPermissionAuthorizeAttribute> GetPermissionAuthorizeAttributes(params Assembly[] assemblys)
        {
            var result = new List<IPermissionAuthorizeAttribute>();

            // 遍历程序集
            foreach (var assembly in assemblys)
            {
                // 获取程序集中的所有类型
                var types = assembly.GetExportedTypes()
                    .Where(o => !o.IsAbstract
                         && !o.IsInterface
                         && !o.IsGenericType
                         && o.IsClass
                         )
                    ;

                // 遍历类型
                foreach (var type in types)
                {
                    // 获取类型中所有的函数并遍历
                    var methodInfos = type.GetMethods();
                    foreach (var methodInfo in methodInfos)
                    {
                        // 获取方法上的 PermissionAuthorizeAttribute 特性
                        var attrs = methodInfo
                            .GetCustomAttributes(false)
                            .Where(o =>
                            {
                                return o.GetType()
                                .GetInterface(PermissionAuthorizeAttributeType.Name) != null;
                            }).ToArray()
                            ;
                        if (attrs.Length == 0)
                        {
                            continue;
                        }

                        // 将特性中的 Permission 数据加入字典
                        var attr = attrs[0] as IPermissionAuthorizeAttribute;


                        result.Add(attr);
                    }
                }


            }

            return result;
        }

        protected virtual IEnumerable<PermissionItem> AttrToItems(IEnumerable<IPermissionAuthorizeAttribute> input)
        {
            // Permission 字典集合
            var permissionDict = new Dictionary<PermissionInfo, PermissionAuthorizeScope>();

            var newItem = default(PermissionInfo);

            // attr 转 info
            foreach (var attr in input)
            {
                foreach (var permission in attr.Permissions)
                {
                    newItem = new PermissionInfo(permission, attr.Scope, attr.Sort);
                    if (permissionDict.ContainsKey(newItem))
                    {
                        continue;
                    }
                    permissionDict[newItem] = attr.Scope;
                }
            }

            // 将数据进行分组
            var groupPermissions = permissionDict.GroupBy(o => o.Value)
                .Select(o =>
                {
                    return new
                    {
                        Scope = o.Key,
                        Permissions = o.AsEnumerable().Select(p => p.Key)
                    };
                })
                .ToList();

            // 遍历分组后的数据并生成结构添加到管理器
            var permissionItemDict = new Dictionary<PermissionItem, string>();

            // 默认的根节点
            permissionItemDict.Add(
                new PermissionItem(
                    IdentityPermissionConsts.ROOT_NODE,
                    null,
                    0,
                    PermissionAuthorizeScope.Common),
                string.Empty
                );

            // info 转 item
            foreach (var groupPermission in groupPermissions)
            {
                foreach (var permission in groupPermission.Permissions)
                {
                    foreach (var permissionItem in PermissionToTree(permission, IdentityPermissionConsts.ROOT_NODE))
                    {
                        if (permissionItemDict.ContainsKey(permissionItem))
                        {
                            continue;
                        }

                        permissionItemDict.Add(permissionItem, string.Empty);
                    }
                }
            }


            return permissionItemDict.Keys;
        }

        /// <summary>
        /// Permission 转树形数据结构
        /// </summary>
        /// <param name="permission"></param>
        /// <param name="scope"></param>
        /// <param name="defaultParent"></param>
        /// <returns></returns>
        protected static List<PermissionItem> PermissionToTree(PermissionInfo permissionInfo, string defaultParent = null)
        {
            var result = new List<PermissionItem>();
            var permissionArray = PermissionToArray(permissionInfo.Name).Reverse<string>().ToList();
            if (permissionArray.Count == 0)
            {
                return result;
            }


            var maxCount = permissionArray.Count;
            var index = 0;
            var parentIndex = index + 1;
            while (true)
            {
                var newPermission = permissionArray[index];
                if (maxCount == parentIndex)
                {
                    result.Add(
                        new PermissionItem(
                            newPermission,
                            defaultParent,
                            permissionInfo.Sort,
                            permissionInfo.Scope
                            )
                        );
                    break;
                }

                result.Add(
                       new PermissionItem(
                            newPermission,
                            permissionArray[parentIndex],
                            permissionInfo.Sort,
                            permissionInfo.Scope
                            )
                       );

                index++;
                parentIndex = index + 1;
            }


            return result;
        }

        /// <summary>
        /// Permission 转数组，如 "user.create" 输出 ["user","user.create"]
        /// </summary>
        /// <param name="permission">claim</param>
        /// <returns></returns>
        protected static List<string> PermissionToArray(string permission)
        {
            var result = new List<string>();

            var index = 0;
            while (true)
            {
                index = permission.IndexOf(".", index);
                if (index == -1)
                {
                    result.Add(permission);
                    break;
                }

                var subStr = permission.Substring(0, index);
                result.Add(subStr);
                index += 1;
            }

            return result;
        }

        /// <summary>
        /// 权限信息
        /// </summary>
        protected class PermissionInfo
        {
            public virtual string Name { get; protected set; }

            public virtual PermissionAuthorizeScope Scope { get; protected set; }

            public virtual int Sort { get; protected set; }

            public virtual int HashCode { get; protected set; }

            public PermissionInfo(string name, PermissionAuthorizeScope scope, int sort)
            {
                Name = name;
                Scope = scope;
                Sort = sort;

                HashCode = name.GetHashCode() + scope.GetHashCode();
            }

            public override bool Equals(object obj)
            {
                if (obj == null)
                {
                    return false;
                }

                if (obj is PermissionItem input)
                {
                    return input.GetHashCode() == this.GetHashCode();
                }

                return false;
            }

            public override int GetHashCode()
            {
                return this.HashCode;
            }
        }
    }
}
