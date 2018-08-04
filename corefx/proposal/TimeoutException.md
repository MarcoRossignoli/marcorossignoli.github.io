## Rationale
I'm working on a project where i need to heavily use TimeoutException.  
Today we cannot pass TimeSpan to [TimeoutException](https://source.dot.net/#System.Private.CoreLib/shared/System/TimeoutException.cs,43e40a96805cc4df), like ParamName for ArgumentException.
Could be useful add overloads to pass TimeSpan when throw exception, this could be useful for some reason:  

  * improve default exception message(we could append timespan if present)
  * we could improve retry logic based on timeout that we cannot change directly or we don't know

## Proposed API

```cs
public class TimeoutException
{
    public TimeoutException(TimeSpan timeout) {}
    public TimeoutException(string message, TimeSpan timeout) {}
    public TimeoutException(string message, TimeSpan timeout, Exception innerException) {}
    public virtual TimeSpan? Timeout { get; }
}
```
## Extra

[I didn't found any issue with this on repo](https://github.com/dotnet/corefx/issues?utf8=%E2%9C%93&q=TimeoutException+TimeSpan) , but i suspect that this is not new topic.
I found on codebase an implementation for [RegEx](https://source.dot.net/#System.Text.RegularExpressions/System/Text/RegularExpressions/RegexMatchTimeoutException.cs,74)