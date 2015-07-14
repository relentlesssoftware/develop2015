# develop2015
This is the code drop to support the Develop 2015 talk called "Cloud Surfing" from Relentless Software.  
This repository includes an Open Source version of our Provider Pattern for Unity&reg; so feel free to 
use it in your own projects.

However, this project is not in any way endorsed or supervised by Unity Technologies.

_Unity&reg; is a trademark of Unity Technologies._

# Overview
The *Provider Pattern* is loosely based on the [Strategy Pattern](https://en.wikipedia.org/wiki/Strategy_pattern), 
but has been adapted for use in Unity.

This pattern can also be combined with the [Singleton Pattern](https://en.wikipedia.org/wiki/Singleton_pattern).

## Uses
The Provider Pattern
* is used to abstract away 3rd party plugin code or systems 
* helps to protect your game code from changes
* allows you to aggregate to multiple providers
* can decide at runtime which provider to use
* allows you to easily swap out plugins (or systems) when you need to

## How To
* Derive a new Manager class which is the main interface that the game uses to access the plugin or system.
For instance, you can create an AnalyticsProviderManager that is what the game uses to log telemetry events.
* Implement a new Provider class that uses the plugin. You can place any specific data, like Access Keys, etc. in here too.
# Add both the Manager and the Provider to the same GameObject
# Now only use the MAnager in your game code - don't access the providers directly.

## Examples
* In the project you'll find a few examples. This is just to show you how the providers are used in Unity. Below you can find a screen shot too.
* We also provided a unitypackage for Unity 5 that you can import

![Dummy Provider](https://s3-eu-west-1.amazonaws.com/temporaryfiles/DummyProviderUnity.PNG)

# Analytics Provider UML diagram
Here is a UML diagram of how we set up our analytics providers.

![AnalyticsProvider](https://s3-eu-west-1.amazonaws.com/temporaryfiles/ProviderPatternAnalytics.png)
