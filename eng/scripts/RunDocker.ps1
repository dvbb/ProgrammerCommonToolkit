$copyDirectory = Resolve-Path (Join-Path -Path $PSScriptRoot -ChildPath "../../src/ProgrammerToolkit.Backend/")

if (Test-Path $copyDirectory)
{
    Set-Location  $copyDirectory
}

# create a docker image named ToolKit
docker build -f Dockerfile .. -t ToolKit
