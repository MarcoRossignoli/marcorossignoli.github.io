Git useful links

* Squash remote commits https://gist.github.com/patik/b8a9dc5cd356f9f6f980#combining-the-commits  
i.e. squash last 2 commit  
```
git reset --soft HEAD~2
git commit -m "commit message"
git push origin +currentBranchName
```
* Replace local branch with remote branch entirely https://stackoverflow.com/questions/9210446/replace-local-branch-with-remote-branch-entirely   
```
git reset --hard origin/<current branch name>
```  
* Small guide https://medium.com/datadriveninvestor/git-for-beginner-f438adfc3599  
* Configure upstream fork https://help.github.com/articles/configuring-a-remote-for-a-fork/  
* Update upstream/origin  
```
git fetch upstream
git checkout main
git merge upstream/main
git push origin main
```

Or rebase https://github.com/dotnet/corefx/pull/29751#issuecomment-390033889

* Setup credendial git https://git-scm.com/docs/git-credential-store
```
$ git config credential.helper store
$ git push http://example.com/repo.git
Username: <type your username>
Password: <type your password>

[several days later]
$ git push http://example.com/repo.git
[your credentials are used automatically]
```

* git-deleting-old-local-branches unix http://erikaybar.name/git-deleting-old-local-branches/
``` git
git fetch -p
git branch -vv | grep 'origin/.*: gone]' | awk '{print $1}' | xargs git branch -d
```

* git-deleting-old-local-branches windows
``` git
git fetch -p
powershell "git branch --list --format '%(if:equals=[gone])%(upstream:track)%(then)%(refname)%(end)' |  ? { $_ -ne '' } | % { $_ -replace '^refs/heads/', '' } | % { git branch -D $_ }"
```

* Block push for upstream for safety   
```
git remote set-url --push upstream no_push
```

* Bring branch to fork
```
git checkout main
git checkout --track -b release/7.0.2xx upstream/release/7.0.2xx  <- first name is the one we want on origin the second the upstream one
git push origin <- push it on origin
```
