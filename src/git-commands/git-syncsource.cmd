git checkout master
git gc
git fetch -p
git pull
powershell "git branch --list --format '%%(if:equals=[gone])%%(upstream:track)%%(then)%%(refname)%%(end)' |  ? { $_ -ne '' } | %% { $_ -replace '^refs/heads/', '' } | %% { git branch -D $_ }"

@rem To install on Win
@rem git config --global alias.syncsource "!C:/git/git-custom-commands/git-syncsource.cmd"