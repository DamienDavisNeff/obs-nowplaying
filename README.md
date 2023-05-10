# Now Playing for OBS

This program uses [WindowsMediaController](https://github.com/DubyaDude/WindowsMediaController/) to save the title, artist, and art of the song you are currently listening to as text/images, for use in OBS.
![image](placeholder_image)

This allows you to add a now playing area to your OBS scene.
***
## Requirements
- Windows 10 (Build 17763+) or Windows 11
- .NET 4.6+ or .NET Core 3.0+ or .NET 5+
***
## How To Use
1) Download The `exe` From [Releases](placeholder_link)
2) Store It In A Safe Spot (Remember It For Later)
3) Run The `exe` & Player Music At Least Once
4) Open OBS Studio
5) Add 2 Texts Source (For `artist` & `title`)
![image](placeholder_image)
6) Add 1 Image Source (For `album` art)
![image](placeholder_image)
7) Apply The `Scaling/Aspect Ratio` Filter To The Image
![image](placeholder_image)
8) Set The Resolution To A Resolution (NOT A RATIO) With An Aspect Ratio Of 1:1
    - Do not select the 1:1 (or any) ratio, ensure you are choosing a resolution
    - This ensures scaling stays consistent no matter what
![image](placeholder_image)
9) Open Properties For A Text Source
10) Select `Read From File`
![image](placeholder_image)
11) Press Browse
12) Locate `artist.txt` or `title.txt`
    - These will be in the same location as the `exe` from earlier
13) Repeat Steps 8-11 For The Other Thing
14) Open Properties For Your Image Source
15) Click `Browse`
16) Locate `album.png`
    - This will be in the location as the `exe` from earlier

***
**This is the barebones version, album art is still buggy, and documentaion is missing images
