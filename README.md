SimpleAudioManager

A simple Controller to dynamically create AudioManagers.

A Sound-Aray manages the amount of Sound-Clips you want to create, the Controller then creates AudioManager's for each clip automatically. Each sound-clip can be played when called by it's string-name => Play("Name").

The Controller has 3 states...

    When game starts (do smth when scene started has started)
    When sounds is activated (do smth when sound was activated by user)
    When sounds are manipulated (do smth when sound was manipulated by user)

... which can be modified to perform AudioMixer effects.
