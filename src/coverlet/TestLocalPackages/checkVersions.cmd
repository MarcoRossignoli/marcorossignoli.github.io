git cleanup
nuget restore -verbosity detailed > vers.txt
findstr /n /s /c:"coverlet." vers.txt
nuget sources
