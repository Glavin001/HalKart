#Ouya Unity Template
1. Read and follow [ODK Setup Guide](https://devs.ouya.tv/developers/docs/setup) up to Eclipse heading
2. Install [Android NDK](http://developer.android.com/tools/sdk/ndk/index.html)
3. Install Java 1.6 32 bit  -  [Windows](http://www.oracle.com/technetwork/java/javasebusiness/downloads/java-archive-downloads-javase6-419409.html#jdk-6u45-oth-JPR)  -  [Mac](http://support.apple.com/kb/DL1572?viewlocale=en_US)
  * Windows 8 can use 1.7

#Usage
1. Clone this repo
2. Open the project(folder) in Unity
3. Open the unity panel ```Window -> Open Ouya Panel```
4. Set your java, Android SDK and NDK paths, if they are greyed out something is incorrect.
5. Check your build settings (Android platform) and make sure OuyaInit is always the first scene
6. In the Ouya tab click these buttons in order, Compile, Compile Java, Compile Plugin, Compile NDK
7. Connect Ouya, Build and Run
