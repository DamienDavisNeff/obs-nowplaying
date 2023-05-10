# Now Playing for OBS

This program uses <a href="https://github.com/DubyaDude/WindowsMediaController/" target="_blank">WindowsMediaController</a> to save the title, artist, and art of the song you are currently listening to as text/images, for use in OBS.

<img alt="Windows Media Controls: Now Playing" src="https://github.com/DamienDavisNeff/obs-nowplaying/assets/105996288/ce2f19e1-4f41-4710-a866-f75fe395fdef" style="height: 10vw">
<img alt="OBS Studio showing what is playing in Windows Media Controls" src="https://github.com/DamienDavisNeff/obs-nowplaying/assets/105996288/89d9feda-8ce3-4f3d-a017-44b43e071381" style="height: 10vw">


This allows you to add a now playing area to your OBS scene.
***
## Requirements
- Windows 10 (Build 17763+) or Windows 11
- .NET 4.6+ or .NET Core 3.0+ or <a href="https://dotnet.microsoft.com/en-us/download" target="_blank">.NET 5+</a>
***
## How To Use
1) Download `now-playing.exe` from <a href="https://github.com/DamienDavisNeff/obs-nowplaying/releases" target="_blank">Releases</a>

<img alt="Download page for this project" src="https://github.com/DamienDavisNeff/obs-nowplaying/assets/105996288/a0f66729-3117-4005-8198-9a4ff46bbdf8" style="height:15vw">

2) Store it in a safe spot (and remember it for later)
3) Run `now-playing.exe` at least once, and playback some form of media
4) Open `OBS Studio`
5) Add 2 text sources (for `artist` & `title`)

<img alt="Adding a text source in OBS Studio" src="https://github.com/DamienDavisNeff/obs-nowplaying/assets/105996288/95ffd545-0c1e-41eb-9bfc-6b0071ffe2e6" style="height:15vw">
<img alt="Naming the text source ''Artists''" src="https://github.com/DamienDavisNeff/obs-nowplaying/assets/105996288/fdce8ec6-2b31-45d7-98f0-eaf1653bcea1" style="height:15vw">
<img alt="Naming the text source ''Title''" src="https://github.com/DamienDavisNeff/obs-nowplaying/assets/105996288/e8b372e0-07a0-4c16-af4a-bd5f0532b253" style="height:15vw">

6) Add 1 image source (For `album` art)

<img alt="Adding an image source in OBS Studio" src="https://github.com/DamienDavisNeff/obs-nowplaying/assets/105996288/a2fb3309-9fa6-401b-bc1f-1cf36b18d7fc" style="height:15vw">
<img alt="Naming the image source ''album''" src="https://github.com/DamienDavisNeff/obs-nowplaying/assets/105996288/8e701a88-7340-4101-9725-83e2283b92a7" style="height:15vw">

7) Apply the `Scaling/Aspect Ratio` filter to your image & set the resolution to anything with an aspect ratio of 1:1 (both numbers the same)
     - Do not select the 1:1 (or any) ratio, ensure you are choosing a resolution
     - This ensures scaling stays consistent

<img alt="Opening the filters menu in OBS Studio" src="https://github.com/DamienDavisNeff/obs-nowplaying/assets/105996288/49fccd09-73dd-429f-8ac0-91e6edd35321" style="height:10vw">
<img alt="Applying the Scaling/Aspect Ratio filter" src="https://github.com/DamienDavisNeff/obs-nowplaying/assets/105996288/84bf7651-ed7d-435e-a99a-c87abf0f3b29" style="height:10vw">
<img alt="Setting the resolution to 800x800" src="https://github.com/DamienDavisNeff/obs-nowplaying/assets/105996288/7508e459-9a4e-43b4-8988-8887fc3f0ce8" style="height:10vw">

8) Open `Properties` for your text source & select `Read From File`, then press `Browse` to locate `artist/title.txt`
     - These will be in the same location as the `exe` from earlier
     - Do this for both `artist` and `title`

<img alt="Opening the source properties for a text source" src="https://github.com/DamienDavisNeff/obs-nowplaying/assets/105996288/0f8be81b-845b-47b7-8986-75c8ac1583d7" style="height:10vw">
<img alt="Selecting read from file in text properties" src="https://github.com/DamienDavisNeff/obs-nowplaying/assets/105996288/6e067842-8d97-4485-b18c-4176e12d5211" style="height:10vw">
<img alt="Selecting browse in text properties" src="https://github.com/DamienDavisNeff/obs-nowplaying/assets/105996288/8265dca8-4826-457e-a8bd-c73d3a767d5d" style="height:10vw">

9) Open `Properties` for your image source, then press `Browse` to locate `album.png`
    - This will be in the location as the `exe` from earlier

<img alt="Opening the source properties for a image source" src="https://github.com/DamienDavisNeff/obs-nowplaying/assets/105996288/52fafe33-2f98-4f50-8d57-b9190b09ef8c" style="height:10vw">
<img alt="Selecting browse in image properties" src="https://github.com/DamienDavisNeff/obs-nowplaying/assets/105996288/8265dca8-4826-457e-a8bd-c73d3a767d5d" style="height:10vw">

***
*imgs are currently broken in readme, as the repo is now public - working on fixing them*
