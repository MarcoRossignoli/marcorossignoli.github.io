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
## Usage
```cs

while(true)
{
    ...retry logic...
    try
    {
        // print does some works...supervision printer, read RFID, set parameters, print something
        // every operation could timeout, but supervision timeout are recoverable other 
        // not always or we cannot wait too much(if timeout is for example > 30 seconds)
        printer.Print();
    }
    catch(TimeoutException ex)
    {
        // we can recover supervision timeout
        if (ex.Timeout.HasValue && ex.Timeout <= TimeSpan.FromSeconds(3))
        {
            continue;
        }
        else
            throw;
    }
}

```
```cs
TimeSpan timeoutWait = TimeSpan.Zero;
while (true)
{
    try
    {
        return printer.GetStatus();
    }
    catch (TimeoutException ex)
    {
        if (ex.Timeout.HasValue)
        {
            timeoutWait = timeoutWait.Add(ex.Timeout.Value);

            // Wait for no more than 30 seconds
            // I know timeout could be > 30 sec, but retry as much as possible, best we can
            // Timeout threshold could change in future
            // Also in subclassed exception scenario...threshold could change          
            if (timeoutWait >= TimeSpan.FromSeconds(30))
                throw;

            continue;
        }
    }
}
```
## Extra

[I didn't found any issue with this on repo](https://github.com/dotnet/corefx/issues?utf8=%E2%9C%93&q=TimeoutException+TimeSpan) , but i suspect that this is not new topic.
I found on codebase an implementation for [RegEx](https://source.dot.net/#System.Text.RegularExpressions/System/Text/RegularExpressions/RegexMatchTimeoutException.cs,74)