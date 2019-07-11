## Wrapped .NET Target APIs for STTP

.NET APIs wrap [STTP C++ API](https://github.com/sttp/cppapi)

#### Change Log

1.0.8 - 2019-07-11

    Updated to cppapi v1.0.14 which improved lock handling of signal index cache, reconnect timing for data subscribers and data publisher port-in-use exception reporting from v1.0.13.

1.0.7 - 2019-07-08

    Updated to cppapi v1.0.12 which improved shutdown speed when auto-reconnect is active.

1.0.6 - 2019-07-04

    Updated to cppapi v1.0.11 which fixed subscriber instance reconnect after disconnect and added extra null check for cases when publisher provides no metadata.

1.0.5 - 2019-07-03

    Updated to cppapi v1.0.10 which included fixes for race conditions around startup, shutdown, reconnect and shared reference dispatch operations.

1.0.4 - 2019-07-02

    Updated to latest cppapi which included data subscriber updates for improved shutdown operation while auto-reconnect is in progress.

1.0.3 - 2019-07-01

    Updated to latest cppapi which included data subscriber fixes for auto-reconnect logic and data publisher fixes for null metadata.

1.0.2 - 2019-06-29
    
    Changed SignalIndexCache.Count() method to be exposed in .NET as a property

1.0.1 - 2019-06-28

    Added SubscriptionUpdated virtual method for Subscriber access to signal index cache updates.

1.0.0 - 2019-06-27

    Initial release