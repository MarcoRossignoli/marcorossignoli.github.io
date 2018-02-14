Git useful links

* resolve "This branch has conflicts that must be resolved" online git https://github.com/githubteacher/github-for-developers-sept-2015/issues/648

* Replace local branch with remote branch entirely https://stackoverflow.com/questions/9210446/replace-local-branch-with-remote-branch-entirely

* Replace master branch with upstream https://stackoverflow.com/questions/42332769/how-do-i-reset-the-git-master-branch-to-the-upstream-branch-in-a-forked-reposito

* Overwrite single file in my current branch with the same file in the master branch https://stackoverflow.com/questions/13847425/overwrite-single-file-in-my-current-branch-with-the-same-file-in-the-master-bran

* Clean up a fork and restart it from the upstream https://stackoverflow.com/questions/9646167/clean-up-a-fork-and-restart-it-from-the-upstream

* issue keyword  https://help.github.com/articles/closing-issues-using-keywords/
 If there would still be some more work to do then including something like contributes to #24817 would both link to the issue and add a link to here from that issue, which would make finding that some work had been done on it more discoverable by someone looking at the issue.

* rename git branch locally and remotely https://gist.github.com/lttlrck/9628955

* Update upstream/origin
```
    git fetch upstream </br>
    git checkout master </br>
    git merge upstream/master </br>
    git push origin master </br>
```
* Squash remote commits https://gist.github.com/patik/b8a9dc5cd356f9f6f980#combining-the-commits
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