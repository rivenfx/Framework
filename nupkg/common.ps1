# 路径
$packFolder = (Get-Item -Path "./" -Verbose).FullName   # 当前路径
$rootFolder = Join-Path $packFolder "../"               # 项目根目录
$packOutputFolder = Join-Path $packFolder "dist"        # 输出nuget package 目录



# 所有的项目名称
$projects = (
    "Riven",
    "Riven.Dependency",
    "Riven.AspectCore",
    "Riven.AspNetCore",
    "Riven.AspNetCore.Identity",
    "Riven.AspNetCore.Localization",
    "Riven.AspNetCore.Modular",
    "Riven.AspNetCore.Swashbuckle",
    "Riven.AspNetCore.Uow",
    "Riven.Domain",
    "Riven.Domain.EntityFrameworkCore",
    "Riven.Localization",
    "Riven.Mapster",
    "Riven.UnitOfWork",
    "Riven.UnitOfWork.Dapper",
    "Riven.UnitOfWork.EntityFrameworkCore"
)
