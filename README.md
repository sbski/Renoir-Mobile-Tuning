# Renoir Mobile Tuning
## Unlock your Ryzen 4000 processor
Enables control of power and temperature limits on AMD Renoir powered laptops.
Idea for it was as a temperary hold over untill my bigger app is done being fixed for Renoir. RyzenAdj is working now without GPU and SOC clock speeds but Renoir does a good job of managing those now. Renoir-only version of Ryzen Controller. Uses Flygoat's smu-tool found here: https://github.com/flygoat/ryzen_nb_smu
## Installation
- Go to the releases tab found [here](https://github.com/sbski/Renoir-Mobile-Tuning/releases) and download the latest release
- Extract the files and place them were you would like to.
- You are good to go.
## Helpful tips on keeping settings applied
- Try to change your power profile that your OEM suplies to the highest perfomance mode
  - This is known to help on HP's and Asus Laptops for sure
  - Putting your computer to sleep resets most of the settings
- I will be creating a new project based on this with monitoring tools, profiles, auto apply, and a few other things. It will also support Raven-Ridge and Picasso directly. 

If anyone would like to help with that I would greatly appreciate any help. I think it will be a mix of c/c++ and c# for most of the code.
Join the [Ryzen Controller Discord](https://discordapp.com/invite/EahayUv) if you want to keep up with deveolopment or talk with like-minded people!
