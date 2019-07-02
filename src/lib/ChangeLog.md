
## Wrapped .NET Target APIs for STTP

.NET APIs wrap [STTP C++ API](https://github.com/sttp/cppapi)

#### Change Log

1.0.4 - 2019-07-02

    Updated to latest `cppapi` which included data subscriber updates for improved shutdown operation while auto-reconnect is in progress.

1.0.3 - 2019-07-01

    Updated to latest `cppapi` which included data subscriber fixes for auto-reconnect logic and data publisher fixes for null metadata.

1.0.2 - 2019-06-29
    
    Changed SignalIndexCache.Count() method to be exposed in .NET as a property

1.0.1 - 2019-06-28

    Added SubscriptionUpdated virtual method for Subscriber access to signal index cache updates.

1.0.0 - 2019-06-27

    Initial release