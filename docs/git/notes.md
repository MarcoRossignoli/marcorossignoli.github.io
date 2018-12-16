Git useful links

* Squash remote commits https://gist.github.com/patik/b8a9dc5cd356f9f6f980#combining-the-commits  
* Replace local branch with remote branch entirely https://stackoverflow.com/questions/9210446/replace-local-branch-with-remote-branch-entirely   
* Small guide https://medium.com/datadriveninvestor/git-for-beginner-f438adfc3599  
* Update upstream/origin  
```
    git fetch upstream
    git checkout master
    git merge upstream/master
    git push origin master
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

