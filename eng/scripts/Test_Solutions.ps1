$srcPath = "$PSScriptRoot/../../src"

# Run all tests
$solutions = Get-ChildItem $srcPath
$solutions  | ForEach-Object {
    write-host $_.Name run all unit tests:
    & cd $_.PSPath
    & dotnet test
}