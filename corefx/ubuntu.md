* setup rdp https://docs.microsoft.com/en-us/azure/virtual-machines/linux/use-remote-desktop </br>
* fix xrdp visual studio code https://github.com/Microsoft/vscode/issues/3451#issuecomment-217716116 </br>
* install chrome https://www.ubuntuupdates.org/ppa/google_chrome </br>
* fix tag issue https://askubuntu.com/questions/352121/bash-auto-completion-with-xubuntu-and-xrdp-from-windows?answertab=votes#tab-top </br>
* configure keyboard "dpkg-reconfigure keyboard-configuration" </br>
* debug vs code https://github.com/dotnet/corefx/blob/e548aec5876e1d8233809451fb5113b20d874be6/Documentation/debugging/unix-instructions.md#using-visual-studio-code <br/>
* fix debugger attach issue https://github.com/OmniSharp/omnisharp-vscode/issues/1979#issuecomment-362110246 <br/>
* Stephen Toub dev advice [PR](https://github.com/dotnet/corefx/pull/27411#issuecomment-368120730)

what do you use to dev coreclr/corefx on unix?

I generally just use Visual Studio on Windows. I've got WSL set up and do lots of Unix testing locally. If I need to target a particular distro/version, I often set up a VM in Azure and ssh into it, then use vim.
