# FluentRequestTamperer
Fluent request tamperer is a generic wrapper for FiddlerCore library. Created to make writing exploits easier.

An exploit is a software that takes advantage of a bug or vulnerability to cause unintended or unanticipated behavior to occur on computer software, hardware, or something electronic.

This library makes it easier to catch requests from machine and edit them before they are sent. In order to make everything work, you will need to setup a proxy for outgoing reqests from attacked application. For some applications you will be able to set this in their settings. If that is not possible, then you can set the proxy globally in Windows proxy settings:

![alt text](https://raw.githubusercontent.com/duszakpawel/FluentRequestTamperer/master/proxy_setup.png)

Set the proxy host to 127.0.0.1 and port to 8888. Make sure your settings are just like in the picture above.

DISCLAIMER:

The creators give up their rights and responsibilities for everything will happen due to use of this software. Make sure all you want to do is legal.

You can find some examples of use under Demo project.
