@IF [%1] == [] GOTO MissingCommitMessage

git add .
git commit -m %1
git push

@goto End

:MissingCommitMessage
@echo Missing commit message
@goto End

:End

@rem To install on Win
@rem git config --global alias.commitall "!C:/git/git-custom-commands/git-commitall.cmd"