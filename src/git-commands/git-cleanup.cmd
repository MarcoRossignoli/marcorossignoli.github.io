taskkill /IM dotnet.exe /F
taskkill /IM VBCSCompiler.exe /F
taskkill /IM msbuild.exe /F
git clean -fdx

@rem To install on Win
@rem git config --global alias.cleanup "!C:/git/marcorossignoli.github.io/src/git-commands/git-cleanup.cmd"