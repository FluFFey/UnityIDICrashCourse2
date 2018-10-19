# UnityIDICrashCourse2

Det ligger noen hjelpescript under Scripts/Utilities

MusicCaller hjelper litt med musikk (spør om noe er uklart (noe kommer til å være uklart))

SoundCaller hjelper litt med lyd (spør om noe er uklart)

Timer er en hjelpeklasse for bruk av timers
Eks:
Timer respawnTimer = new Timer(5); //parameter er duration på timer
respawnTimer.hasended(); //true om timeren har endt
respawnTimer.restart(); //Restarter timer.. restartes ikke automatisk

Tools har noen funksjoner for flytting og rotering av objekter. Kan være litt wonky i kombinasjon med Rigidbodies
Ganske selvforklarende parametre, men spør om det er noe interessant. Unity har også lignende innebygde funskjoner, men mener at de er litt dårlige.
