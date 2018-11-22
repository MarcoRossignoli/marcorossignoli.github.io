@IF [%1] == [] GOTO MissingCommitMessage

@for /f %%i in ('git branch ^| grep \* ^| cut -d ' ' -f2') do @set BRANCHNAME=%%i
@for /f %%i in ('git rev-list --count HEAD') do @set THISBRANCHCOUNT=%%i
@for /f %%i in ('git rev-list --count master') do @set MASTERCOUNT=%%i
@set /a "TOTALCOMMIT=%THISBRANCHCOUNT%-%MASTERCOUNT%"

@echo Total new commit on %BRANCHNAME% = %TOTALCOMMIT%

@if %TOTALCOMMIT% LSS 0 (
    @goto InvalidTotalCommitCount
)

git reset --soft HEAD~%TOTALCOMMIT%
git commit -m %1
git push origin +%BRANCHNAME%

@set "THISBRANCHCOUNT="
@set "MASTERCOUNT="
@set "TOTALCOMMIT="
@set "BRANCHNAME="

@goto End

:MissingCommitMessage
@echo Missing commit message
@goto End

:InvalidTotalCommitCount
@echo Invalid TOTALCOMMIT %TOTALCOMMIT% 
@goto End

:End

@rem To install on Win
@rem git config --global alias.squashall "!C:/git/git-custom-commands/git-squash.cmd"