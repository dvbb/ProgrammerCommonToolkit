$srcPath = "$PSScriptRoot/../../src"

# restore and build all sln
$solutions = Get-ChildItem $srcPath
$solutions  | ForEach-Object {
    write-host $_.Name Start build:
    & cd $_.PSPath
    & dotnet restore
    & dotnet build
}