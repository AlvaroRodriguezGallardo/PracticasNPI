# First practice
## Oral chatbot for the study plans of ETSIIT

We have considereted two degrees: Degree in Computer Engineering and Degree in Telecomunications Engineering.

Firstly it is done a prototype in spanish. It could be generalize in other languages.

As programme languaje it is used `VoiceXML` and it is run in a platform called 'Voxeo': all files are in that platform and we make a call with `MicroSip`.

In `Voxeo`, all files are uploaded to a folder called `www`. We upload `es_PlanEstudios.xml`, and all grammars in folder `Español`. After this, we make an application with a name, for example, 'PI_NPI_ES', we select `Voice Phone Calls` and options `Development/Europe/VoiceXML/Nuance/EU - Prophecy VoiceXML`.

Now, in Contact Methods a table can be seen. We copy the direction in column `Number`, third row, and paste it in `Microsip`. For example, if we have `	sip:9996123409@sip.lhr.aspect-cloud.net`, then we use `9996123409@sip.lhr.aspect-cloud.net`. 
