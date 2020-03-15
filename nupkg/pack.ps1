# 相关路径
$packFolder = (Get-Item -Path "./" -Verbose).FullName

$packFolderModule = Join-Path $packFolder "framework"
$slnPath = Join-Path $packFolder "../"
$srcPath = Join-Path $slnPath "src"

# 创建文件夹
 rm -Force -Recurse  $packFolderModule
 mkdir $packFolderModule


# 所有的项目名称
$projects = (
    "Riven",
    "Riven.Domain"
)

# 重新生成项目
Set-Location $slnPath
& dotnet restore

# 创建并移动过所有的 nuget 包到输出目录
foreach($project in $projects) {
    
    # 拼接项目目录
    $projectFolder = Join-Path $srcPath $project

    # 创建 nuget 包
    Set-Location $projectFolder
    Get-ChildItem (Join-Path $projectFolder "bin/Release") -ErrorAction SilentlyContinue | Remove-Item -Recurse
    & dotnet msbuild /p:Configuration=Release /p:SourceLinkCreate=true
    & dotnet msbuild /t:pack /p:Configuration=Release /p:SourceLinkCreate=true

    # 复制 nuget 包
    $projectPackPath = Join-Path $projectFolder ("/bin/Release/" + $project + ".*.nupkg")
    Move-Item $projectPackPath $packFolderModule

}

# 返回脚本启动目录
Set-Location $packFolder