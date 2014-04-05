HipchatRedisRelay
=================
So this is part of the pipeline looks like this


![Part 1 Diagram](http://i.imgur.com/xM0QtXw.png)

K so for part 2, I'm assuming we subscribed to the hipchatMessage channel in our bot application, and receiving messages.

First step is probably a parser, that takes the Message the user sent, parses out the command if any.  If there is a command is 
directed at us (idk, maybe if they include @Botso, or maybe just Botso at the begging of the message).

So a message like

``Botso show me order #51848``

The parser would see that this is a command to us, and would parse out the command, which in this case would just be

``show me order #51848``.

The next step would be some sort of dispatcher.

This would receive the messages, and hand them off to whatever service needs to handle them.

I see a couple different types of commands.

Global commands - These are available everywhere, and get handled first
Room specific commands - These commands only work in specific rooms
Custom commands - These are the majority of commands that are typically handled by scripts.


So the dispatcher would first check to see if the command is a global command (idk maybe ``botso shutdown`` or ``botso restart``)

##Custom Commands##

I believe we should have some sort of registry for scripts, possibly just use Redis cache storage for these.  They key/value would be

``Key = Pattern, Value = ScriptName.js``

So if we had an entry that looked like

``Key = '^hello botso$', Value = 'sayhello.js'``

When a command matches that regex pattern, the ``sayhello.js`` script would run.

Inside the ``sayhello.js`` script you might have something like 

```js
//get a variable botso has exposed to us
var fromUser = Botso.fromUser;

//this function is exposed to the scripts, and will send a message to hipchat
sendMessage(Botso.fromRoomId, "Hello " + fromUser);

//indicate to botso that it should still process other scripts 
//if other scripts also match this pattern
Botso.continueProcessing = true;
```


So following this example, if someone says 'botso hello botso' in a room, the parser would first parse out the command
``hello botso``, the dispatcher would find what scripts match this pattern (in this case ``sayhello.js``) and would execute
this script.

In hipchat it would look like:

```

Kyle Gobel: botso hello botso
Botso: Hello Kyle Gobel

```


Anyway this is just rough draft idea of how i'm envisioning it.
```
