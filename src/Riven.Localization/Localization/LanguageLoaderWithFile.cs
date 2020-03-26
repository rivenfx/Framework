using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.IO;
using Newtonsoft.Json;

namespace Riven.Localization
{
    /// <summary>
    /// 语言信息加载器 - 文件
    /// </summary>
    public static class LanguageLoaderWithFile
    {
        const string extensionName = ".json";
        static Encoding UTF8WithNoBom = new UTF8Encoding(false);

        public static IList<LanguageInfo> FromFolderWithJson([NotNull]string folderAbsolutePath)
        {
            Check.NotNullOrWhiteSpace(folderAbsolutePath, nameof(folderAbsolutePath));

            var filePathList = Directory.GetFiles(folderAbsolutePath)
                .Where(o => o.ToLower().EndsWith(extensionName))
                .ToList();

            if (filePathList.Count == 0)
            {
                return null;
            }

            var result = new List<LanguageInfo>();

            foreach (var filePath in filePathList)
            {
                var fileContent = File.ReadAllText(filePath, UTF8WithNoBom);
                result.Add(
                    JsonConvert.DeserializeObject<LanguageInfo>(fileContent)
                );
            }

            return result;
        }
    }
}
