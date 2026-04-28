using Console;
using iiMenu.Classes;
using iiMenu.Extensions;
using iiMenu.Menu;
using iiMenu.Mods.Spammers;
using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using static iiMenu.Menu.Main;

/*
 *  HEY SKIDDERS
 *  you can take the code ig
*/

namespace iiMenu.Mods
{
    internal class Settings
    {
        public static void AnnoyingModeOff()
        {
            annoyingMode = false;
            themeType--;
            ChangeMenuTheme();
        }


        public static void ChangeMenuTheme(bool increment = true)
        {
            if (increment)
                themeType++;
            else
                themeType--;

            int themeCount = 65;

            if (themeType > themeCount)
                themeType = 1;

            if (themeType < 1)
                themeType = themeCount;

            switch (themeType)
            {
                case 1: // Orange
                    backgroundColor = new ExtGradient
                    {
                        colors = ExtGradient.GetSimpleGradient(new Color32(255, 128, 0, 128), new Color32(255, 102, 0, 128))
                    };
                    buttonColors = new[]
                    {
                        new ExtGradient // Released
                        {
                            colors = ExtGradient.GetSolidGradient(new Color32(170, 85, 0, 255))
                        },
                        new ExtGradient // Pressed
                        {
                            colors = ExtGradient.GetSolidGradient(new Color32(85, 42, 0, 255))
                        }
                    };
                    textColors = new[]
                    {
                        new ExtGradient // Title
                        {
                            colors = ExtGradient.GetSolidGradient(new Color32(255, 190, 125, 255))
                        },
                        new ExtGradient // Button Released
                        {
                            colors = ExtGradient.GetSolidGradient(new Color32(255, 190, 125, 255))
                        },
                        new ExtGradient // Button Clicked
                        {
                            colors = ExtGradient.GetSolidGradient(new Color32(255, 190, 125, 255))
                        }
                    };
                    break;
                case 2: // Blue Magenta
                    backgroundColor = new ExtGradient
                    {
                        colors = ExtGradient.GetSimpleGradient(Color.blue, Color.magenta)
                    };
                    buttonColors = new[]
                    {
                        new ExtGradient // Released
                        {
                            colors = ExtGradient.GetSolidGradient(Color.black)
                        },
                        new ExtGradient // Pressed
                        {
                            colors = ExtGradient.GetSolidGradient(Color.blue)
                        }
                    };
                    textColors = new[]
                    {
                        new ExtGradient // Title
                        {
                            colors = ExtGradient.GetSolidGradient(Color.white)
                        },
                        new ExtGradient // Button Released
                        {
                            colors = ExtGradient.GetSolidGradient(Color.white)
                        },
                        new ExtGradient // Button Clicked
                        {
                            colors = ExtGradient.GetSolidGradient(Color.white)
                        }
                    };
                    break;
                case 3: // Dark Mode
                    backgroundColor = new ExtGradient
                    {
                        colors = ExtGradient.GetSolidGradient(Color.black)
                    };
                    buttonColors = new[]
                    {
                        new ExtGradient // Released
                        {
                            colors = ExtGradient.GetSolidGradient(new Color32(50, 50, 50, 255))
                        },
                        new ExtGradient // Pressed
                        {
                            colors = ExtGradient.GetSolidGradient(new Color32(20, 20, 20, 255))
                        }
                    };
                    textColors = new[]
                    {
                        new ExtGradient // Title
                        {
                            colors = ExtGradient.GetSolidGradient(Color.white)
                        },
                        new ExtGradient // Button Released
                        {
                            colors = ExtGradient.GetSolidGradient(Color.white)
                        },
                        new ExtGradient // Button Clicked
                        {
                            colors = ExtGradient.GetSolidGradient(Color.white)
                        }
                    };
                    break;
                case 4: // Strobe
                    backgroundColor = new ExtGradient
                    {
                        colors = ExtGradient.GetSimpleGradient(Color.white, Color.black)
                    };
                    buttonColors = new[]
                    {
                        new ExtGradient // Released
                        {
                            colors = ExtGradient.GetSimpleGradient(Color.black, Color.white)
                        },
                        new ExtGradient // Pressed
                        {
                            colors = ExtGradient.GetSolidGradient(Color.white)
                        }
                    };
                    textColors = new[]
                    {
                        new ExtGradient // Title
                        {
                            colors = ExtGradient.GetSolidGradient(Color.black)
                        },
                        new ExtGradient // Button Released
                        {
                            colors = ExtGradient.GetSolidGradient(Color.black)
                        },
                        new ExtGradient // Button Clicked
                        {
                            colors = ExtGradient.GetSolidGradient(Color.black)
                        }
                    };
                    break;
                case 5: // Kman
                    backgroundColor = new ExtGradient
                    {
                        colors = ExtGradient.GetSimpleGradient(Color.black, new Color32(110, 0, 0, 255))
                    };
                    buttonColors = new[]
                    {
                        new ExtGradient // Released
                        {
                            colors = ExtGradient.GetSimpleGradient(Color.black, new Color32(110, 0, 0, 255))
                        },
                        new ExtGradient // Pressed
                        {
                            colors = ExtGradient.GetSolidGradient(new Color32(110, 0, 0, 255))
                        }
                    };
                    textColors = new[]
                    {
                        new ExtGradient // Title
                        {
                            colors = ExtGradient.GetSolidGradient(Color.white)
                        },
                        new ExtGradient // Button Released
                        {
                            colors = ExtGradient.GetSolidGradient(Color.white)
                        },
                        new ExtGradient // Button Clicked
                        {
                            colors = ExtGradient.GetSolidGradient(Color.white)
                        }
                    };
                    break;
                case 6: // Rainbow
                    backgroundColor = new ExtGradient
                    {
                        colors = ExtGradient.GetSolidGradient(Color.black),
                        rainbow = true
                    };
                    buttonColors = new[]
                    {
                        new ExtGradient // Released
                        {
                            colors = ExtGradient.GetSolidGradient(Color.black)
                        },
                        new ExtGradient // Pressed
                        {
                            colors = ExtGradient.GetSolidGradient(Color.black),
                            rainbow = true
                        }
                    };
                    textColors = new[]
                    {
                        new ExtGradient // Title
                        {
                            colors = ExtGradient.GetSolidGradient(Color.white)
                        },
                        new ExtGradient // Button Released
                        {
                            colors = ExtGradient.GetSolidGradient(Color.white)
                        },
                        new ExtGradient // Button Clicked
                        {
                            colors = ExtGradient.GetSolidGradient(Color.white)
                        }
                    };
                    break;
                case 7: // 2nd Orange
                    backgroundColor = new ExtGradient
                    {
                        colors = ExtGradient.GetSolidGradient(new Color32(255, 128, 0, 128))
                    };
                    buttonColors = new[]
                    {
                        new ExtGradient // Released
                        {
                            colors = ExtGradient.GetSolidGradient(new Color32(170, 85, 0, 255))
                        },
                        new ExtGradient // Pressed
                        {
                            colors = ExtGradient.GetSolidGradient(new Color32(85, 42, 0, 255))
                        }
                    };
                    textColors = new[]
                    {
                        new ExtGradient // Title
                        {
                            colors = ExtGradient.GetSolidGradient(new Color32(255, 190, 125, 255))
                        },
                        new ExtGradient // Button Released
                        {
                            colors = ExtGradient.GetSolidGradient(new Color32(255, 190, 125, 255))
                        },
                        new ExtGradient // Button Clicked
                        {
                            colors = ExtGradient.GetSolidGradient(new Color32(255, 190, 125, 255))
                        }
                    };
                    GetIndex("Thin Menu").enabled = true;
                    thinmenu = true;
                    break;
                case 8: // Player Material
                    backgroundColor = new ExtGradient
                    {
                        colors = ExtGradient.GetSolidGradient(Color.black),
                        copyRigColor = true
                    };
                    buttonColors = new[]
                    {
                        new ExtGradient // Released
                        {
                            colors = ExtGradient.GetSolidGradient(Color.black)
                        },
                        new ExtGradient // Pressed
                        {
                            colors = ExtGradient.GetSolidGradient(Color.black),
                            copyRigColor = true
                        }
                    };
                    textColors = new[]
                    {
                        new ExtGradient // Title
                        {
                            colors = ExtGradient.GetSolidGradient(Color.white)
                        },
                        new ExtGradient // Button Released
                        {
                            colors = ExtGradient.GetSolidGradient(Color.white)
                        },
                        new ExtGradient // Button Clicked
                        {
                            colors = ExtGradient.GetSolidGradient(Color.white)
                        }
                    };
                    break;
                case 9: // Lava
                    backgroundColor = new ExtGradient
                    {
                        colors = ExtGradient.GetSimpleGradient(Color.black, new Color32(255, 111, 0, 255))
                    };
                    buttonColors = new[]
                    {
                        new ExtGradient // Released
                        {
                            colors = ExtGradient.GetSolidGradient(Color.black)
                        },
                        new ExtGradient // Pressed
                        {
                            colors = ExtGradient.GetSimpleGradient(new Color32(255, 111, 0, 255), Color.black)
                        }
                    };
                    textColors = new[]
                    {
                        new ExtGradient // Title
                        {
                            colors = ExtGradient.GetSolidGradient(Color.white)
                        },
                        new ExtGradient // Button Released
                        {
                            colors = ExtGradient.GetSolidGradient(Color.white)
                        },
                        new ExtGradient // Button Clicked
                        {
                            colors = ExtGradient.GetSolidGradient(Color.white)
                        }
                    };
                    break;
                case 10: // Rock
                    backgroundColor = new ExtGradient
                    {
                        colors = ExtGradient.GetSimpleGradient(Color.black, Color.red)
                    };
                    buttonColors = new[]
                    {
                        new ExtGradient // Released
                        {
                            colors = ExtGradient.GetSolidGradient(Color.black)
                        },
                        new ExtGradient // Pressed
                        {
                            colors = ExtGradient.GetSimpleGradient(Color.red, Color.black)
                        }
                    };
                    textColors = new[]
                    {
                        new ExtGradient // Title
                        {
                            colors = ExtGradient.GetSolidGradient(Color.white)
                        },
                        new ExtGradient // Button Released
                        {
                            colors = ExtGradient.GetSolidGradient(Color.white)
                        },
                        new ExtGradient // Button Clicked
                        {
                            colors = ExtGradient.GetSolidGradient(Color.white)
                        }
                    };
                    break;
                case 11: // Ice
                    backgroundColor = new ExtGradient
                    {
                        colors = ExtGradient.GetSimpleGradient(Color.black, new Color32(0, 174, 255, 255))
                    };
                    buttonColors = new[]
                    {
                        new ExtGradient // Released
                        {
                            colors = ExtGradient.GetSolidGradient(Color.black)
                        },
                        new ExtGradient // Pressed
                        {
                            colors = ExtGradient.GetSimpleGradient(new Color32(0, 174, 255, 255), Color.black)
                        }
                    };
                    textColors = new[]
                    {
                        new ExtGradient // Title
                        {
                            colors = ExtGradient.GetSolidGradient(Color.white)
                        },
                        new ExtGradient // Button Released
                        {
                            colors = ExtGradient.GetSolidGradient(Color.white)
                        },
                        new ExtGradient // Button Clicked
                        {
                            colors = ExtGradient.GetSolidGradient(Color.white)
                        }
                    };
                    break;
                case 12: // Water
                    backgroundColor = new ExtGradient
                    {
                        colors = ExtGradient.GetSimpleGradient(new Color32(0, 136, 255, 255), new Color32(0, 174, 255, 255))
                    };
                    buttonColors = new[]
                    {
                        new ExtGradient // Released
                        {
                            colors = ExtGradient.GetSolidGradient(new Color32(0, 100, 188, 255))
                        },
                        new ExtGradient // Pressed
                        {
                            colors = ExtGradient.GetSimpleGradient(new Color32(0, 174, 255, 255), new Color32(0, 136, 255, 255))
                        }
                    };
                    textColors = new[]
                    {
                        new ExtGradient // Title
                        {
                            colors = ExtGradient.GetSolidGradient(Color.black)
                        },
                        new ExtGradient // Button Released
                        {
                            colors = ExtGradient.GetSolidGradient(Color.black)
                        },
                        new ExtGradient // Button Clicked
                        {
                            colors = ExtGradient.GetSolidGradient(Color.black)
                        }
                    };
                    break;
                case 13: // Minty
                    backgroundColor = new ExtGradient
                    {
                        colors = ExtGradient.GetSimpleGradient(new Color32(0, 255, 246, 255), new Color32(0, 255, 144, 255))
                    };
                    buttonColors = new[]
                    {
                        new ExtGradient // Released
                        {
                            colors = ExtGradient.GetSolidGradient(Color.white)
                        },
                        new ExtGradient // Pressed
                        {
                            colors = ExtGradient.GetSimpleGradient(new Color32(0, 255, 144, 255), new Color32(0, 255, 246, 255))
                        }
                    };
                    textColors = new[]
                    {
                        new ExtGradient // Title
                        {
                            colors = ExtGradient.GetSolidGradient(Color.black)
                        },
                        new ExtGradient // Button Released
                        {
                            colors = ExtGradient.GetSolidGradient(Color.black)
                        },
                        new ExtGradient // Button Clicked
                        {
                            colors = ExtGradient.GetSolidGradient(Color.black)
                        }
                    };
                    break;
                case 14: // Pink
                    backgroundColor = new ExtGradient
                    {
                        colors = ExtGradient.GetSimpleGradient(new Color32(255, 130, 255, 255), Color.white)
                    };
                    buttonColors = new[]
                    {
                        new ExtGradient // Released
                        {
                            colors = ExtGradient.GetSolidGradient(new Color32(255, 130, 255, 255))
                        },
                        new ExtGradient // Pressed
                        {
                            colors = ExtGradient.GetSolidGradient(Color.white)
                        }
                    };
                    textColors = new[]
                    {
                        new ExtGradient // Title
                        {
                            colors = ExtGradient.GetSolidGradient(Color.black)
                        },
                        new ExtGradient // Button Released
                        {
                            colors = ExtGradient.GetSolidGradient(Color.black)
                        },
                        new ExtGradient // Button Clicked
                        {
                            colors = ExtGradient.GetSolidGradient(Color.black)
                        }
                    };
                    break;
                case 15: // Purple
                    backgroundColor = new ExtGradient
                    {
                        colors = ExtGradient.GetSimpleGradient(new Color32(122, 35, 159, 255), new Color32(60, 26, 89, 255))
                    };
                    buttonColors = new[]
                    {
                        new ExtGradient // Released
                        {
                            colors = ExtGradient.GetSolidGradient(new Color32(60, 26, 89, 255))
                        },
                        new ExtGradient // Pressed
                        {
                            colors = ExtGradient.GetSolidGradient(new Color32(122, 35, 159, 255))
                        }
                    };
                    textColors = new[]
                    {
                        new ExtGradient // Title
                        {
                            colors = ExtGradient.GetSolidGradient(Color.white)
                        },
                        new ExtGradient // Button Released
                        {
                            colors = ExtGradient.GetSolidGradient(Color.white)
                        },
                        new ExtGradient // Button Clicked
                        {
                            colors = ExtGradient.GetSolidGradient(Color.white)
                        }
                    };
                    break;
                case 16: // Magenta Cyan
                    backgroundColor = new ExtGradient
                    {
                        colors = ExtGradient.GetSimpleGradient(Color.magenta, Color.cyan)
                    };
                    buttonColors = new[]
                    {
                        new ExtGradient // Released
                        {
                            colors = ExtGradient.GetSolidGradient(Color.black)
                        },
                        new ExtGradient // Pressed
                        {
                            colors = ExtGradient.GetSimpleGradient(Color.magenta, Color.cyan)
                        }
                    };
                    textColors = new[]
                    {
                        new ExtGradient // Title
                        {
                            colors = ExtGradient.GetSolidGradient(Color.white)
                        },
                        new ExtGradient // Button Released
                        {
                            colors = ExtGradient.GetSolidGradient(Color.white)
                        },
                        new ExtGradient // Button Clicked
                        {
                            colors = ExtGradient.GetSolidGradient(Color.black)
                        }
                    };
                    break;
                case 17: // Red Fade
                    backgroundColor = new ExtGradient
                    {
                        colors = ExtGradient.GetSimpleGradient(Color.red, Color.black)
                    };
                    buttonColors = new[]
                    {
                        new ExtGradient // Released
                        {
                            colors = ExtGradient.GetSolidGradient(Color.black)
                        },
                        new ExtGradient // Pressed
                        {
                            colors = ExtGradient.GetSolidGradient(Color.red)
                        }
                    };
                    textColors = new[]
                    {
                        new ExtGradient // Title
                        {
                            colors = ExtGradient.GetSolidGradient(Color.red)
                        },
                        new ExtGradient // Button Released
                        {
                            colors = ExtGradient.GetSolidGradient(Color.red)
                        },
                        new ExtGradient // Button Clicked
                        {
                            colors = ExtGradient.GetSolidGradient(Color.black)
                        }
                    };
                    break;
                case 18: // Orange Fade
                    backgroundColor = new ExtGradient
                    {
                        colors = ExtGradient.GetSimpleGradient(new Color32(255, 128, 0, 255), Color.black)
                    };
                    buttonColors = new[]
                    {
                        new ExtGradient // Released
                        {
                            colors = ExtGradient.GetSolidGradient(Color.black)
                        },
                        new ExtGradient // Pressed
                        {
                            colors = ExtGradient.GetSolidGradient(new Color32(255, 128, 0, 255))
                        }
                    };
                    textColors = new[]
                    {
                new ExtGradient // Title
                        {
                            colors = ExtGradient.GetSolidGradient(new Color32(255, 128, 0, 255))
                        },
                        new ExtGradient // Button Released
                        {
                            colors = ExtGradient.GetSolidGradient(new Color32(255, 128, 0, 255))
                        },
                        new ExtGradient // Button Clicked
                        {
                            colors = ExtGradient.GetSolidGradient(Color.black)
                        }
                    };
                    break;
                case 19: // Yellow Fade
                    backgroundColor = new ExtGradient
                    {
                        colors = ExtGradient.GetSimpleGradient(Color.yellow, Color.black)
                    };
                    buttonColors = new[]
                    {
                        new ExtGradient // Released
                        {
                            colors = ExtGradient.GetSolidGradient(Color.black)
                        },
                        new ExtGradient // Pressed
                        {
                            colors = ExtGradient.GetSolidGradient(Color.yellow)
                        }
                    };
                    textColors = new[]
                    {
                        new ExtGradient // Title
                        {
                            colors = ExtGradient.GetSolidGradient(Color.yellow)
                        },
                        new ExtGradient // Button Released
                        {
                            colors = ExtGradient.GetSolidGradient(Color.yellow)
                        },
                        new ExtGradient // Button Clicked
                        {
                            colors = ExtGradient.GetSolidGradient(Color.black)
                        }
                    };
                    break;
                case 20: // Green Fade
                    backgroundColor = new ExtGradient
                    {
                        colors = ExtGradient.GetSimpleGradient(Color.green, Color.black)
                    };
                    buttonColors = new[]
                    {
                        new ExtGradient // Released
                        {
                            colors = ExtGradient.GetSolidGradient(Color.black)
                        },
                        new ExtGradient // Pressed
                        {
                            colors = ExtGradient.GetSolidGradient(Color.green)
                        }
                    };
                    textColors = new[]
                    {
                        new ExtGradient // Title
                        {
                            colors = ExtGradient.GetSolidGradient(Color.green)
                        },
                        new ExtGradient // Button Released
                        {
                            colors = ExtGradient.GetSolidGradient(Color.green)
                        },
                        new ExtGradient // Button Clicked
                        {
                            colors = ExtGradient.GetSolidGradient(Color.black)
                        }
                    };
                    break;
                case 21: // Blue Fade
                    backgroundColor = new ExtGradient
                    {
                        colors = ExtGradient.GetSimpleGradient(Color.blue, Color.black)
                    };
                    buttonColors = new[]
                    {
                        new ExtGradient // Released
                        {
                            colors = ExtGradient.GetSolidGradient(Color.black)
                        },
                        new ExtGradient // Pressed
                        {
                            colors = ExtGradient.GetSolidGradient(Color.blue)
                        }
                    };
                    textColors = new[]
                    {
                        new ExtGradient // Title
                        {
                            colors = ExtGradient.GetSolidGradient(Color.blue)
                        },
                        new ExtGradient // Button Released
                        {
                            colors = ExtGradient.GetSolidGradient(Color.blue)
                        },
                        new ExtGradient // Button Clicked
                        {
                            colors = ExtGradient.GetSolidGradient(Color.black)
                        }
                    };
                    break;
                case 22: // Purple Fade
                    backgroundColor = new ExtGradient
                    {
                        colors = ExtGradient.GetSimpleGradient(new Color32(119, 0, 255, 255), Color.black)
                    };
                    buttonColors = new[]
                    {
                        new ExtGradient // Released
                        {
                            colors = ExtGradient.GetSolidGradient(Color.black)
                        },
                        new ExtGradient // Pressed
                        {
                            colors = ExtGradient.GetSolidGradient(new Color32(119, 0, 255, 255))
                        }
                    };
                    textColors = new[]
                    {
                        new ExtGradient // Title
                        {
                            colors = ExtGradient.GetSolidGradient(new Color32(119, 0, 255, 255))
                        },
                        new ExtGradient // Button Released
                        {
                            colors = ExtGradient.GetSolidGradient(new Color32(119, 0, 255, 255))
                        },
                        new ExtGradient // Button Clicked
                        {
                            colors = ExtGradient.GetSolidGradient(Color.black)
                        }
                    };
                    break;
                case 23: // Magenta Fade
                    backgroundColor = new ExtGradient
                    {
                        colors = ExtGradient.GetSimpleGradient(Color.magenta, Color.black)
                    };
                    buttonColors = new[]
                    {
                        new ExtGradient // Released
                        {
                            colors = ExtGradient.GetSolidGradient(Color.black)
                        },
                        new ExtGradient // Pressed
                        {
                            colors = ExtGradient.GetSolidGradient(Color.magenta)
                        }
                    };
                    textColors = new[]
                    {
                        new ExtGradient // Title
                        {
                            colors = ExtGradient.GetSolidGradient(Color.magenta)
                        },
                        new ExtGradient // Button Released
                        {
                            colors = ExtGradient.GetSolidGradient(Color.magenta)
                        },
                        new ExtGradient // Button Clicked
                        {
                            colors = ExtGradient.GetSolidGradient(Color.black)
                        }
                    };
                    break;
                case 24: // Banana
                    backgroundColor = new ExtGradient
                    {
                        colors = ExtGradient.GetSimpleGradient(new Color32(255, 255, 130, 255), Color.white)
                    };
                    buttonColors = new[]
                    {
                        new ExtGradient // Released
                        {
                            colors = ExtGradient.GetSolidGradient(Color.white)
                        },
                        new ExtGradient // Pressed
                        {
                            colors = ExtGradient.GetSolidGradient(new Color32(255, 255, 130, 255))
                        }
                    };
                    textColors = new[]
                    {
                        new ExtGradient // Title
                        {
                            colors = ExtGradient.GetSolidGradient(Color.black)
                        },
                        new ExtGradient // Button Released
                        {
                            colors = ExtGradient.GetSolidGradient(Color.black)
                        },
                        new ExtGradient // Button Clicked
                        {
                            colors = ExtGradient.GetSolidGradient(Color.black)
                        }
                    };
                    break;
                case 25: // Pride
                    backgroundColor = new ExtGradient
                    {
                        colors = ExtGradient.GetSimpleGradient(Color.red, Color.green)
                    };
                    buttonColors = new[]
                    {
                        new ExtGradient // Released
                        {
                            colors = ExtGradient.GetSolidGradient(Color.black)
                        },
                        new ExtGradient // Pressed
                        {
                            colors = ExtGradient.GetSolidGradient(Color.white)
                        }
                    };
                    textColors = new[]
                    {
                        new ExtGradient // Title
                        {
                            colors = ExtGradient.GetSolidGradient(Color.black)
                        },
                        new ExtGradient // Button Released
                        {
                            colors = ExtGradient.GetSolidGradient(Color.white)
                        },
                        new ExtGradient // Button Clicked
                        {
                            colors = ExtGradient.GetSolidGradient(Color.black)
                        }
                    };
                    break;
                case 26: // Trans
                    backgroundColor = new ExtGradient
                    {
                        colors = ExtGradient.GetSimpleGradient(new Color32(245, 169, 184, 255), new Color32(91, 206, 250, 255))
                    };
                    buttonColors = new[]
                    {
                        new ExtGradient // Released
                        {
                            colors = ExtGradient.GetSolidGradient(new Color32(245, 169, 184, 255))
                        },
                        new ExtGradient // Pressed
                        {
                            colors = ExtGradient.GetSolidGradient(new Color32(91, 206, 250, 255))
                        }
                    };
                    textColors = new[]
                    {
                        new ExtGradient // Title
                        {
                            colors = ExtGradient.GetSolidGradient(new Color32(91, 206, 250, 255))
                        },
                        new ExtGradient // Button Released
                        {
                            colors = ExtGradient.GetSolidGradient(new Color32(91, 206, 250, 255))
                        },
                        new ExtGradient // Button Clicked
                        {
                            colors = ExtGradient.GetSolidGradient(new Color32(245, 169, 184, 255))
                        }
                    };
                    break;
                case 27: // MLM or Gay
                    backgroundColor = new ExtGradient
                    {
                        colors = ExtGradient.GetSimpleGradient(new Color32(7, 141, 112, 255), new Color32(61, 26, 220, 255))
                    };
                    buttonColors = new[]
                    {
                        new ExtGradient // Released
                        {
                            colors = ExtGradient.GetSolidGradient(new Color32(7, 141, 112, 255))
                        },
                        new ExtGradient // Pressed
                        {
                            colors = ExtGradient.GetSolidGradient(new Color32(61, 26, 220, 255))
                        }
                    };
                    textColors = new[]
                    {
                        new ExtGradient // Title
                        {
                            colors = ExtGradient.GetSolidGradient(new Color32(61, 26, 220, 255))
                        },
                        new ExtGradient // Button Released
                        {
                            colors = ExtGradient.GetSolidGradient(new Color32(61, 26, 220, 255))
                        },
                        new ExtGradient // Button Clicked
                        {
                            colors = ExtGradient.GetSolidGradient(new Color32(7, 141, 112, 255))
                        }
                    };
                    break;
                case 28: // Steal (old)
                    backgroundColor = new ExtGradient
                    {
                        colors = ExtGradient.GetSolidGradient(new Color32(50, 50, 50, 255))
                    };
                    buttonColors = new[]
                    {
                        new ExtGradient // Released
                        {
                            colors = ExtGradient.GetSolidGradient(new Color32(50, 50, 50, 255))
                        },
                        new ExtGradient // Pressed
                        {
                            colors = ExtGradient.GetSolidGradient(new Color32(75, 75, 75, 255))
                        }
                    };
                    textColors = new[]
                    {
                        new ExtGradient // Title
                        {
                            colors = ExtGradient.GetSolidGradient(Color.white)
                        },
                        new ExtGradient // Button Released
                        {
                            colors = ExtGradient.GetSolidGradient(Color.white)
                        },
                        new ExtGradient // Button Clicked
                        {
                            colors = ExtGradient.GetSolidGradient(Color.white)
                        }
                    };
                    break;
                case 29: // Silence
                    backgroundColor = new ExtGradient
                    {
                        colors = ExtGradient.GetSimpleGradient(Color.black, new Color32(80, 0, 80, 255))
                    };
                    buttonColors = new[]
                    {
                        new ExtGradient // Released
                        {
                            colors = ExtGradient.GetSolidGradient(Color.black)
                        },
                        new ExtGradient // Pressed
                        {
                            colors = ExtGradient.GetSolidGradient(Color.black)
                        }
                    };
                    textColors = new[]
                    {
                        new ExtGradient // Title
                        {
                            colors = ExtGradient.GetSolidGradient(Color.white)
                        },
                        new ExtGradient // Button Released
                        {
                            colors = ExtGradient.GetSolidGradient(Color.white)
                        },
                        new ExtGradient // Button Clicked
                        {
                            colors = ExtGradient.GetSolidGradient(Color.green)
                        }
                    };
                    break;
                case 30: // Transparent
                    backgroundColor = new ExtGradient
                    {
                        colors = ExtGradient.GetSolidGradient(Color.black),
                        transparent = true
                    };
                    buttonColors = new[]
                    {
                        new ExtGradient // Released
                        {
                            colors = ExtGradient.GetSolidGradient(Color.white),
                            transparent = true
                        },
                        new ExtGradient // Pressed
                        {
                            colors = ExtGradient.GetSolidGradient(Color.green),
                            transparent = true
                        }
                    };
                    textColors = new[]
                    {
                        new ExtGradient // Title
                        {
                            colors = ExtGradient.GetSolidGradient(Color.white)
                        },
                        new ExtGradient // Button Released
                        {
                            colors = ExtGradient.GetSolidGradient(Color.white)
                        },
                        new ExtGradient // Button Clicked
                        {
                            colors = ExtGradient.GetSolidGradient(Color.green)
                        }
                    };
                    break;
                case 31: // King
                    backgroundColor = new ExtGradient
                    {
                        colors = ExtGradient.GetSolidGradient(new Color32(100, 60, 170, 255))
                    };
                    buttonColors = new[]
                    {
                        new ExtGradient // Released
                        {
                            colors = ExtGradient.GetSolidGradient(new Color32(150, 100, 240, 255))
                        },
                        new ExtGradient // Pressed
                        {
                            colors = ExtGradient.GetSolidGradient(new Color32(150, 100, 240, 255))
                        }
                    };
                    textColors = new[]
                    {
                        new ExtGradient // Title
                        {
                            colors = ExtGradient.GetSolidGradient(Color.white)
                        },
                        new ExtGradient // Button Released
                        {
                            colors = ExtGradient.GetSolidGradient(Color.white)
                        },
                        new ExtGradient // Button Clicked
                        {
                            colors = ExtGradient.GetSolidGradient(Color.cyan)
                        }
                    };
                    break;
                case 32: // Scoreboard
                    backgroundColor = new ExtGradient
                    {
                        colors = ExtGradient.GetSolidGradient(new Color32(0, 59, 4, 255))
                    };
                    buttonColors = new[]
                    {
                        new ExtGradient // Released
                        {
                            colors = ExtGradient.GetSolidGradient(new Color32(192, 190, 171, 255))
                        },
                        new ExtGradient // Pressed
                        {
                            colors = ExtGradient.GetSolidGradient(Color.red)
                        }
                    };
                    textColors = new[]
                    {
                        new ExtGradient // Title
                        {
                            colors = ExtGradient.GetSolidGradient(Color.white)
                        },
                        new ExtGradient // Button Released
                        {
                            colors = ExtGradient.GetSolidGradient(Color.black)
                        },
                        new ExtGradient // Button Clicked
                        {
                            colors = ExtGradient.GetSolidGradient(Color.black)
                        }
                    };
                    break;
                case 33: // Scoreboard (banned)
                    backgroundColor = new ExtGradient
                    {
                        colors = ExtGradient.GetSolidGradient(new Color32(225, 73, 43, 255))
                    };
                    buttonColors = new[]
                    {
                        new ExtGradient // Released
                        {
                            colors = ExtGradient.GetSolidGradient(new Color32(192, 190, 171, 255))
                        },
                        new ExtGradient // Pressed
                        {
                            colors = ExtGradient.GetSolidGradient(Color.red)
                        }
                    };
                    textColors = new[]
                    {
                        new ExtGradient // Title
                        {
                            colors = ExtGradient.GetSolidGradient(Color.white)
                        },
                        new ExtGradient // Button Released
                        {
                            colors = ExtGradient.GetSolidGradient(Color.black)
                        },
                        new ExtGradient // Button Clicked
                        {
                            colors = ExtGradient.GetSolidGradient(Color.black)
                        }
                    };
                    break;
                case 34: // Rift
                    backgroundColor = new ExtGradient
                    {
                        colors = ExtGradient.GetSolidGradient(new Color32(25, 25, 25, 255))
                    };
                    buttonColors = new[]
                    {
                        new ExtGradient // Released
                        {
                            colors = ExtGradient.GetSolidGradient(new Color32(40, 40, 40, 255))
                        },
                        new ExtGradient // Pressed
                        {
                            colors = ExtGradient.GetSolidGradient(new Color32(167, 66, 191, 255))
                        }
                    };
                    textColors = new[]
                    {
                        new ExtGradient // Title
                        {
                            colors = ExtGradient.GetSolidGradient(new Color32(144, 144, 144, 255))
                        },
                        new ExtGradient // Button Released
                        {
                            colors = ExtGradient.GetSolidGradient(new Color32(144, 144, 144, 255))
                        },
                        new ExtGradient // Button Clicked
                        {
                            colors = ExtGradient.GetSolidGradient(Color.white)
                        }
                    };
                    break;
                case 35: // Blurple Dark
                    backgroundColor = new ExtGradient
                    {
                        colors = ExtGradient.GetSolidGradient(new Color32(26, 26, 61, 255))
                    };
                    buttonColors = new[]
                    {
                        new ExtGradient // Released
                        {
                            colors = ExtGradient.GetSolidGradient(new Color32(26, 26, 61, 255))
                        },
                        new ExtGradient // Pressed
                        {
                            colors = ExtGradient.GetSolidGradient(new Color32(43, 17, 84, 255))
                        }
                    };
                    textColors = new[]
                    {
                        new ExtGradient // Title
                        {
                            colors = ExtGradient.GetSolidGradient(Color.white)
                        },
                        new ExtGradient // Button Released
                        {
                            colors = ExtGradient.GetSolidGradient(Color.white)
                        },
                        new ExtGradient // Button Clicked
                        {
                            colors = ExtGradient.GetSolidGradient(Color.white)
                        }
                    };
                    break;
                case 36: // ShibaGT Gold
                    backgroundColor = new ExtGradient
                    {
                        colors = ExtGradient.GetSimpleGradient(Color.black, Color.gray)
                    };
                    buttonColors = new[]
                    {
                        new ExtGradient // Released
                        {
                            colors = ExtGradient.GetSolidGradient(Color.yellow)
                        },
                        new ExtGradient // Pressed
                        {
                            colors = ExtGradient.GetSolidGradient(Color.magenta)
                        }
                    };
                    textColors = new[]
                    {
                        new ExtGradient // Title
                        {
                            colors = ExtGradient.GetSolidGradient(Color.white)
                        },
                        new ExtGradient // Button Released
                        {
                            colors = ExtGradient.GetSolidGradient(Color.black)
                        },
                        new ExtGradient // Button Clicked
                        {
                            colors = ExtGradient.GetSolidGradient(Color.black)
                        }
                    };
                    break;
                case 37: // ShibaGT Genesis
                    backgroundColor = new ExtGradient
                    {
                        colors = ExtGradient.GetSolidGradient(Color.black)
                    };
                    buttonColors = new[]
                    {
                        new ExtGradient // Released
                        {
                            colors = ExtGradient.GetSolidGradient(new Color32(32, 32, 32, 255))
                        },
                        new ExtGradient // Pressed
                        {
                            colors = ExtGradient.GetSolidGradient(new Color32(32, 32, 32, 255))
                        }
                    };
                    textColors = new[]
                    {
                        new ExtGradient // Title
                        {
                            colors = ExtGradient.GetSolidGradient(Color.white)
                        },
                        new ExtGradient // Button Released
                        {
                            colors = ExtGradient.GetSolidGradient(Color.black)
                        },
                        new ExtGradient // Button Clicked
                        {
                            colors = ExtGradient.GetSolidGradient(Color.white)
                        }
                    };
                    break;
                case 38: // wyvern
                    backgroundColor = new ExtGradient
                    {
                        colors = ExtGradient.GetSimpleGradient(new Color32(199, 115, 173, 255), new Color32(165, 233, 185, 255))
                    };
                    buttonColors = new[]
                    {
                        new ExtGradient // Released
                        {
                            colors = ExtGradient.GetSimpleGradient(new Color32(99, 58, 86, 255), new Color32(83, 116, 92, 255))
                        },
                        new ExtGradient // Pressed
                        {
                            colors = ExtGradient.GetSimpleGradient(new Color32(99, 58, 86, 255), new Color32(83, 116, 92, 255))
                        }
                    };
                    textColors = new[]
                    {
                        new ExtGradient // Title
                        {
                            colors = ExtGradient.GetSolidGradient(Color.white)
                        },
                        new ExtGradient // Button Released
                        {
                            colors = ExtGradient.GetSolidGradient(Color.white)
                        },
                        new ExtGradient // Button Clicked
                        {
                            colors = ExtGradient.GetSolidGradient(Color.green)
                        }
                    };
                    break;
                case 39: // Steal (new)
                    backgroundColor = new ExtGradient
                    {
                        colors = ExtGradient.GetSolidGradient(new Color32(27, 27, 27, 255))
                    };
                    buttonColors = new[]
                    {
                        new ExtGradient // Released
                        {
                            colors = ExtGradient.GetSolidGradient(new Color32(50, 50, 50, 255))
                        },
                        new ExtGradient // Pressed
                        {
                            colors = ExtGradient.GetSolidGradient(new Color32(66, 66, 66, 255))
                        }
                    };
                    textColors = new[]
                    {
                        new ExtGradient // Title
                        {
                            colors = ExtGradient.GetSolidGradient(Color.white)
                        },
                        new ExtGradient // Button Released
                        {
                            colors = ExtGradient.GetSolidGradient(Color.white)
                        },
                        new ExtGradient // Button Clicked
                        {
                            colors = ExtGradient.GetSolidGradient(Color.white)
                        }
                    };
                    break;
                case 40: // USA Menu (lol)
                    backgroundColor = new ExtGradient
                    {
                        colors = ExtGradient.GetSimpleGradient(Color.black, new Color32(100, 25, 125, 255))
                    };
                    buttonColors = new[]
                    {
                        new ExtGradient // Released
                        {
                            colors = ExtGradient.GetSolidGradient(new Color32(25, 25, 25, 255))
                        },
                        new ExtGradient // Pressed
                        {
                            colors = ExtGradient.GetSolidGradient(Color.green)
                        }
                    };
                    textColors = new[]
                    {
                        new ExtGradient // Title
                        {
                            colors = ExtGradient.GetSolidGradient(Color.white)
                        },
                        new ExtGradient // Button Released
                        {
                            colors = ExtGradient.GetSolidGradient(Color.white)
                        },
                        new ExtGradient // Button Clicked
                        {
                            colors = ExtGradient.GetSolidGradient(Color.white)
                        }
                    };
                    break;
                case 41: // Watch
                    backgroundColor = new ExtGradient
                    {
                        colors = ExtGradient.GetSolidGradient(new Color32(27, 27, 27, 255))
                    };
                    buttonColors = new[]
                    {
                        new ExtGradient // Released
                        {
                            colors = ExtGradient.GetSolidGradient(Color.red)
                        },
                        new ExtGradient // Pressed
                        {
                            colors = ExtGradient.GetSolidGradient(Color.green)
                        }
                    };
                    textColors = new[]
                    {
                        new ExtGradient // Title
                        {
                            colors = ExtGradient.GetSolidGradient(Color.white)
                        },
                        new ExtGradient // Button Released
                        {
                            colors = ExtGradient.GetSolidGradient(Color.white)
                        },
                        new ExtGradient // Button Clicked
                        {
                            colors = ExtGradient.GetSolidGradient(Color.white)
                        }
                    };
                    break;
                case 42: // AZ Menu
                    backgroundColor = new ExtGradient
                    {
                        colors = ExtGradient.GetSimpleGradient(Color.black, new Color32(100, 0, 0, 255))
                    };
                    buttonColors = new[]
                    {
                        new ExtGradient // Released
                        {
                            colors = ExtGradient.GetSolidGradient(Color.black)
                        },
                        new ExtGradient // Pressed
                        {
                            colors = ExtGradient.GetSolidGradient(new Color32(100, 0, 0, 255))
                        }
                    };
                    textColors = new[]
                    {
                        new ExtGradient // Title
                        {
                            colors = ExtGradient.GetSolidGradient(Color.white)
                        },
                        new ExtGradient // Button Released
                        {
                            colors = ExtGradient.GetSolidGradient(Color.white)
                        },
                        new ExtGradient // Button Clicked
                        {
                            colors = ExtGradient.GetSolidGradient(Color.white)
                        }
                    };
                    break;
                case 43: // ImGUI
                    backgroundColor = new ExtGradient
                    {
                        colors = ExtGradient.GetSolidGradient(new Color32(21, 22, 23, 255))
                    };
                    buttonColors = new[]
                    {
                        new ExtGradient // Released
                        {
                            colors = ExtGradient.GetSolidGradient(new Color32(32, 50, 77, 255))
                        },
                        new ExtGradient // Pressed
                        {
                            colors = ExtGradient.GetSolidGradient(new Color32(60, 127, 206, 255))
                        }
                    };
                    textColors = new[]
                    {
                        new ExtGradient // Title
                        {
                            colors = ExtGradient.GetSolidGradient(Color.white)
                        },
                        new ExtGradient // Button Released
                        {
                            colors = ExtGradient.GetSolidGradient(Color.white)
                        },
                        new ExtGradient // Button Clicked
                        {
                            colors = ExtGradient.GetSolidGradient(Color.white)
                        }
                    };
                    break;
                case 44: // Clean Dark
                    backgroundColor = new ExtGradient
                    {
                        colors = ExtGradient.GetSolidGradient(Color.black)
                    };
                    buttonColors = new[]
                    {
                        new ExtGradient // Released
                        {
                            colors = ExtGradient.GetSolidGradient(Color.black)
                        },
                        new ExtGradient // Pressed
                        {
                            colors = ExtGradient.GetSolidGradient(new Color32(10, 10, 10, 255))
                        }
                    };
                    textColors = new[]
                    {
                        new ExtGradient // Title
                        {
                            colors = ExtGradient.GetSolidGradient(Color.white)
                        },
                        new ExtGradient // Button Released
                        {
                            colors = ExtGradient.GetSolidGradient(Color.white)
                        },
                        new ExtGradient // Button Clicked
                        {
                            colors = ExtGradient.GetSolidGradient(Color.white)
                        }
                    };
                    break;
                case 45: // Discord Light Mode (lmfao)
                    backgroundColor = new ExtGradient
                    {
                        colors = ExtGradient.GetSolidGradient(Color.white)
                    };
                    buttonColors = new[]
                    {
                        new ExtGradient // Released
                        {
                            colors = ExtGradient.GetSolidGradient(Color.white)
                        },
                        new ExtGradient // Pressed
                        {
                            colors = ExtGradient.GetSolidGradient(new Color32(245, 245, 245, 255))
                        }
                    };
                    textColors = new[]
                    {
                        new ExtGradient // Title
                        {
                            colors = ExtGradient.GetSolidGradient(Color.black)
                        },
                        new ExtGradient // Button Released
                        {
                            colors = ExtGradient.GetSolidGradient(Color.black)
                        },
                        new ExtGradient // Button Clicked
                        {
                            colors = ExtGradient.GetSolidGradient(Color.black)
                        }
                    };
                    break;
                case 46: // The Hub
                    backgroundColor = new ExtGradient
                    {
                        colors = ExtGradient.GetSolidGradient(Color.black)
                    };
                    buttonColors = new[]
                    {
                        new ExtGradient // Released
                        {
                            colors = ExtGradient.GetSolidGradient(new Color32(255, 163, 26, 255))
                        },
                        new ExtGradient // Pressed
                        {
                            colors = ExtGradient.GetSolidGradient(Color.black)
                        }
                    };
                    textColors = new[]
                    {
                        new ExtGradient // Title
                        {
                            colors = ExtGradient.GetSolidGradient(Color.white)
                        },
                        new ExtGradient // Button Released
                        {
                            colors = ExtGradient.GetSolidGradient(Color.black)
                        },
                        new ExtGradient // Button Clicked
                        {
                            colors = ExtGradient.GetSolidGradient(Color.white)
                        }
                    };
                    break;
                case 47: // EPILEPTIC
                    backgroundColor = new ExtGradient
                    {
                        colors = ExtGradient.GetSolidGradient(Color.black),
                        epileptic = true
                    };
                    buttonColors = new[]
                    {
                        new ExtGradient // Released
                        {
                            colors = ExtGradient.GetSolidGradient(Color.black)
                        },
                        new ExtGradient // Pressed
                        {
                            colors = ExtGradient.GetSolidGradient(Color.black),
                            epileptic = true
                        }
                    };
                    textColors = new[]
                    {
                        new ExtGradient // Title
                        {
                            colors = ExtGradient.GetSolidGradient(Color.white)
                        },
                        new ExtGradient // Button Released
                        {
                            colors = ExtGradient.GetSolidGradient(Color.white)
                        },
                        new ExtGradient // Button Clicked
                        {
                            colors = ExtGradient.GetSolidGradient(Color.white)
                        }
                    };
                    break;
                case 48: // Discord Blurple
                    backgroundColor = new ExtGradient
                    {
                        colors = ExtGradient.GetSimpleGradient(new Color32(111, 143, 255, 255), new Color32(163, 184, 255, 255))
                    };
                    buttonColors = new[]
                    {
                        new ExtGradient // Released
                        {
                            colors = ExtGradient.GetSolidGradient(new Color32(96, 125, 219, 255))
                        },
                        new ExtGradient // Pressed
                        {
                            colors = ExtGradient.GetSolidGradient(new Color32(147, 167, 226, 255))
                        }
                    };
                    textColors = new[]
                    {
                        new ExtGradient // Title
                        {
                            colors = ExtGradient.GetSolidGradient(new Color32(33, 33, 101, 255))
                        },
                        new ExtGradient // Button Released
                        {
                            colors = ExtGradient.GetSolidGradient(new Color32(33, 33, 101, 255))
                        },
                        new ExtGradient // Button Clicked
                        {
                            colors = ExtGradient.GetSolidGradient(new Color32(33, 33, 101, 255))
                        }
                    };
                    break;
                case 49: // VS Zero
                    backgroundColor = new ExtGradient
                    {
                        colors = ExtGradient.GetSolidGradient(new Color32(19, 22, 27, 255))
                    };
                    buttonColors = new[]
                    {
                        new ExtGradient // Released
                        {
                            colors = ExtGradient.GetSolidGradient(new Color32(19, 22, 27, 255))
                        },
                        new ExtGradient // Pressed
                        {
                            colors = ExtGradient.GetSolidGradient(new Color32(16, 18, 22, 255))
                        }
                    };
                    textColors = new[]
                    {
                        new ExtGradient // Title
                        {
                            colors = ExtGradient.GetSolidGradient(new Color32(82, 96, 122, 255))
                        },
                        new ExtGradient // Button Released
                        {
                            colors = ExtGradient.GetSolidGradient(new Color32(82, 96, 122, 255))
                        },
                        new ExtGradient // Button Clicked
                        {
                            colors = ExtGradient.GetSolidGradient(new Color32(82, 96, 122, 255))
                        }
                    };
                    break;
                case 50: // Weed theme (for v4.2.0) (also 50th theme)
                    backgroundColor = new ExtGradient
                    {
                        colors = ExtGradient.GetSimpleGradient(new Color32(0, 136, 16, 255), new Color32(0, 127, 14, 255))
                    };
                    buttonColors = new[]
                    {
                        new ExtGradient // Released
                        {
                            colors = ExtGradient.GetSolidGradient(new Color32(0, 158, 15, 255))
                        },
                        new ExtGradient // Pressed
                        {
                            colors = ExtGradient.GetSolidGradient(new Color32(0, 112, 11, 255))
                        }
                    };
                    textColors = new[]
                    {
                        new ExtGradient // Title
                        {
                            colors = ExtGradient.GetSolidGradient(Color.white)
                        },
                        new ExtGradient // Button Released
                        {
                            colors = ExtGradient.GetSolidGradient(Color.white)
                        },
                        new ExtGradient // Button Clicked
                        {
                            colors = ExtGradient.GetSolidGradient(Color.white)
                        }
                    };
                    break;
                case 51: // Pastel Rainbow
                    backgroundColor = new ExtGradient
                    {
                        colors = ExtGradient.GetSolidGradient(Color.white),
                        pastelRainbow = true
                    };
                    buttonColors = new[]
                    {
                        new ExtGradient // Released
                        {
                            colors = ExtGradient.GetSolidGradient(Color.white)
                        },
                        new ExtGradient // Pressed
                        {
                            colors = ExtGradient.GetSolidGradient(Color.white),
                            pastelRainbow = true
                        }
                    };
                    textColors = new[]
                    {
                        new ExtGradient // Title
                        {
                            colors = ExtGradient.GetSolidGradient(Color.black)
                        },
                        new ExtGradient // Button Released
                        {
                            colors = ExtGradient.GetSolidGradient(Color.black)
                        },
                        new ExtGradient // Button Clicked
                        {
                            colors = ExtGradient.GetSolidGradient(Color.black)
                        }
                    };
                    break;
                case 52: // Rift Light
                    backgroundColor = new ExtGradient
                    {
                        colors = ExtGradient.GetSolidGradient(new Color32(25, 25, 25, 255))
                    };
                    buttonColors = new[]
                    {
                        new ExtGradient // Released
                        {
                            colors = ExtGradient.GetSolidGradient(new Color32(40, 40, 40, 255))
                        },
                        new ExtGradient // Pressed
                        {
                            colors = ExtGradient.GetSolidGradient(new Color32(165, 137, 255, 255))
                        }
                    };
                    textColors = new[]
                    {
                        new ExtGradient // Title
                        {
                            colors = ExtGradient.GetSolidGradient(new Color32(144, 144, 144, 255))
                        },
                        new ExtGradient // Button Released
                        {
                            colors = ExtGradient.GetSolidGradient(new Color32(144, 144, 144, 255))
                        },
                        new ExtGradient // Button Clicked
                        {
                            colors = ExtGradient.GetSolidGradient(Color.white)
                        }
                    };
                    break;
                case 53: // Rose (Solace)
                    backgroundColor = new ExtGradient
                    {
                        colors = ExtGradient.GetSolidGradient(new Color32(176, 12, 64, 255))
                    };
                    buttonColors = new[]
                    {
                        new ExtGradient // Released
                        {
                            colors = ExtGradient.GetSolidGradient(new Color32(140, 10, 51, 255))
                        },
                        new ExtGradient // Pressed
                        {
                            colors = ExtGradient.GetSolidGradient(new Color32(250, 2, 81, 255))
                        }
                    };
                    textColors = new[]
                    {
                        new ExtGradient // Title
                        {
                            colors = ExtGradient.GetSolidGradient(Color.white)
                        },
                        new ExtGradient // Button Released
                        {
                            colors = ExtGradient.GetSolidGradient(Color.white)
                        },
                        new ExtGradient // Button Clicked
                        {
                            colors = ExtGradient.GetSolidGradient(Color.white)
                        }
                    };
                    break;
                case 54: // Tenacity (Solace)
                    backgroundColor = new ExtGradient
                    {
                        colors = ExtGradient.GetSolidGradient(new Color32(124, 25, 194, 255))
                    };
                    buttonColors = new[]
                    {
                        new ExtGradient // Released
                        {
                            colors = ExtGradient.GetSolidGradient(new Color32(88, 9, 145, 255))
                        },
                        new ExtGradient // Pressed
                        {
                            colors = ExtGradient.GetSolidGradient(new Color32(136, 9, 227, 255))
                        }
                    };
                    textColors = new[]
                    {
                        new ExtGradient // Title
                        {
                            colors = ExtGradient.GetSolidGradient(Color.white)
                        },
                        new ExtGradient // Button Released
                        {
                            colors = ExtGradient.GetSolidGradient(Color.white)
                        },
                        new ExtGradient // Button Clicked
                        {
                            colors = ExtGradient.GetSolidGradient(Color.white)
                        }
                    };
                    break;
                case 55: // e621 (for version 6.2.1)
                    backgroundColor = new ExtGradient
                    {
                        colors = ExtGradient.GetSolidGradient(new Color32(1, 73, 149, 255))
                    };
                    buttonColors = new[]
                    {
                        new ExtGradient // Released
                        {
                            colors = ExtGradient.GetSolidGradient(new Color32(1, 46, 87, 255))
                        },
                        new ExtGradient // Pressed
                        {
                            colors = ExtGradient.GetSolidGradient(new Color32(0, 37, 74, 255))
                        }
                    };
                    textColors = new[]
                    {
                        new ExtGradient // Title
                        {
                            colors = ExtGradient.GetSolidGradient(new Color32(252, 179, 40, 255))
                        },
                        new ExtGradient // Button Released
                        {
                            colors = ExtGradient.GetSolidGradient(Color.white)
                        },
                        new ExtGradient // Button Clicked
                        {
                            colors = ExtGradient.GetSolidGradient(Color.white)
                        }
                    };
                    break;
                case 56: // Catppuccin Mocha
                    backgroundColor = new ExtGradient
                    {
                        colors = ExtGradient.GetSolidGradient(new Color32(30, 30, 46, 255))
                    };
                    buttonColors = new[]
                    {
                        new ExtGradient // Released
                        {
                            colors = ExtGradient.GetSolidGradient(new Color32(88, 91, 112, 255))
                        },
                        new ExtGradient // Pressed
                        {
                            colors = ExtGradient.GetSolidGradient(new Color32(49, 50, 68, 255))
                        }
                    };
                    textColors = new[]
                    {
                        new ExtGradient // Title
                        {
                            colors = ExtGradient.GetSolidGradient(new Color32(205, 214, 244, 255))
                        },
                        new ExtGradient // Button Released
                        {
                            colors = ExtGradient.GetSolidGradient(new Color32(186, 194, 222, 255))
                        },
                        new ExtGradient // Button Clicked
                        {
                            colors = ExtGradient.GetSolidGradient(new Color32(166, 173, 200, 255))
                        }
                    };
                    break;
                case 57: // Rexon
                    backgroundColor = new ExtGradient
                    {
                        colors = ExtGradient.GetSolidGradient(new Color32(45, 25, 75, 255))
                    };
                    buttonColors = new[]
                    {
                        new ExtGradient // Released
                        {
                            colors = ExtGradient.GetSolidGradient(new Color32(40, 15, 60, 255))
                        },
                        new ExtGradient // Pressed
                        {
                            colors = ExtGradient.GetSolidGradient(new Color32(100, 30, 140, 255))
                        }
                    };
                    textColors = new[]
                    {
                        new ExtGradient // Title
                        {
                            colors = ExtGradient.GetSolidGradient(Color.white)
                        },
                        new ExtGradient // Button Released
                        {
                            colors = ExtGradient.GetSolidGradient(Color.white)
                        },
                        new ExtGradient // Button Clicked
                        {
                            colors = ExtGradient.GetSolidGradient(Color.white)
                        }
                    };
                    break;
                case 58: // Tenacity (Minecraft)
                    backgroundColor = new ExtGradient
                    {
                        colors = ExtGradient.GetSolidGradient(new Color32(32, 32, 32, 255))
                    };
                    buttonColors = new[]
                    {
                        new ExtGradient // Released
                        {
                            colors = ExtGradient.GetSolidGradient(new Color32(45, 46, 51, 255))
                        },
                        new ExtGradient // Pressed
                        {
                            colors = ExtGradient.GetSimpleGradient(new Color32(231, 133, 209, 255), new Color32(56, 155, 193, 255))
                        }
                    };
                    textColors = new[]
                    {
                        new ExtGradient // Title
                        {
                            colors = ExtGradient.GetSolidGradient(Color.white)
                        },
                        new ExtGradient // Button Released
                        {
                            colors = ExtGradient.GetSolidGradient(Color.white)
                        },
                        new ExtGradient // Button Clicked
                        {
                            colors = ExtGradient.GetSolidGradient(Color.white)
                        }
                    };
                    break;
                case 59: // Mint Blue (Opal v2)
                    backgroundColor = new ExtGradient
                    {
                        colors = ExtGradient.GetSolidGradient(new Color32(32, 32, 32, 255))
                    };
                    buttonColors = new[]
                    {
                        new ExtGradient // Released
                        {
                            colors = ExtGradient.GetSolidGradient(new Color32(45, 46, 51, 255))
                        },
                        new ExtGradient // Pressed
                        {
                            colors = ExtGradient.GetSimpleGradient(new Color32(40, 94, 93, 255), new Color32(66, 158, 157, 255))
                        }
                    };
                    textColors = new[]
                    {
                        new ExtGradient // Title
                        {
                            colors = ExtGradient.GetSolidGradient(Color.white)
                        },
                        new ExtGradient // Button Released
                        {
                            colors = ExtGradient.GetSolidGradient(Color.white)
                        },
                        new ExtGradient // Button Clicked
                        {
                            colors = ExtGradient.GetSolidGradient(Color.white)
                        }
            };
                    break;
                case 60: // Pink Blood (Opal v2)
                    backgroundColor = new ExtGradient
                    {
                        colors = ExtGradient.GetSolidGradient(new Color32(32, 32, 32, 255))
                    };
                    buttonColors = new[]
                    {
                        new ExtGradient // Released
                        {
                            colors = ExtGradient.GetSolidGradient(new Color32(45, 46, 51, 255))
                        },
                        new ExtGradient // Pressed
                        {
                            colors = ExtGradient.GetSimpleGradient(new Color32(255, 166, 201, 255), new Color32(228, 0, 70, 255))
                        }
                    };
                    textColors = new[]
                    {
                        new ExtGradient // Title
                        {
                            colors = ExtGradient.GetSolidGradient(Color.white)
                        },
                        new ExtGradient // Button Released
                        {
                            colors = ExtGradient.GetSolidGradient(Color.white)
                        },
                        new ExtGradient // Button Clicked
                        {
                            colors = ExtGradient.GetSolidGradient(Color.white)
                        }
                    };
                    break;
                case 61: // Purple Fire (Opal v2)
                    backgroundColor = new ExtGradient
                    {
                        colors = ExtGradient.GetSolidGradient(new Color32(32, 32, 32, 255))
                    };
                    buttonColors = new[]
                    {
                        new ExtGradient // Released
                        {
                            colors = ExtGradient.GetSolidGradient(new Color32(45, 46, 51, 255))
                        },
                        new ExtGradient // Pressed
                        {
                            colors = ExtGradient.GetSimpleGradient(new Color32(177, 162, 202, 255), new Color32(104, 71, 141, 255))
                        }
                    };
                    textColors = new[]
                    {
                        new ExtGradient // Title
                        {
                            colors = ExtGradient.GetSolidGradient(Color.white)
                        },
                        new ExtGradient // Button Released
                        {
                            colors = ExtGradient.GetSolidGradient(Color.white)
                        },
                        new ExtGradient // Button Clicked
                        {
                            colors = ExtGradient.GetSolidGradient(Color.white)
                        }
                    };
                    break;
                case 62: // Deep Ocean (Opal v2)
                    backgroundColor = new ExtGradient
                    {
                        colors = ExtGradient.GetSolidGradient(new Color32(32, 32, 32, 255))
                    };
                    buttonColors = new[]
                    {
                        new ExtGradient // Released
                        {
                            colors = ExtGradient.GetSolidGradient(new Color32(45, 46, 51, 255))
                        },
                        new ExtGradient // Pressed
                        {
                            colors = ExtGradient.GetSimpleGradient(new Color32(60, 82, 145, 255), new Color32(0, 20, 64, 255))
                        }
                    };
                    textColors = new[]
                    {
                        new ExtGradient // Title
                        {
                            colors = ExtGradient.GetSolidGradient(Color.white)
                        },
                        new ExtGradient // Button Released
                        {
                            colors = ExtGradient.GetSolidGradient(Color.white)
                        },
                        new ExtGradient // Button Clicked
                        {
                            colors = ExtGradient.GetSolidGradient(Color.white)
                        }
                    };
                    break;
                case 63: // Bad Apple (thanks random person in vc for idea)
                    backgroundColor = new ExtGradient
                    {
                        colors = ExtGradient.GetSimpleGradient(Color.black, Color.white)
                    };
                    buttonColors = new[]
                    {
                        new ExtGradient // Released
                        {
                            colors = ExtGradient.GetSolidGradient(Color.white)
                        },
                        new ExtGradient // Pressed
                        {
                            colors = ExtGradient.GetSolidGradient(Color.black)
                        }
                    };
                    textColors = new[]
                    {
                        new ExtGradient // Title
                        {
                            colors = ExtGradient.GetSolidGradient(Color.white)
                        },
                        new ExtGradient // Button Released
                        {
                            colors = ExtGradient.GetSolidGradient(Color.white)
                        },
                        new ExtGradient // Button Clicked
                        {
                            colors = ExtGradient.GetSolidGradient(Color.black)
                        }
                    };
                    break;
                case 64: // coolkidd
                    backgroundColor = new ExtGradient
                    {
                        colors = ExtGradient.GetSolidGradient(Color.red)
                    };
                    buttonColors = new[]
                    {
                        new ExtGradient // Released
                        {
                            colors = ExtGradient.GetSolidGradient(Color.red)
                        },
                        new ExtGradient // Pressed
                        {
                            colors = ExtGradient.GetSolidGradient(Color.black)
                        }
                    };
                    textColors = new[]
                    {
                        new ExtGradient // Title
                        {
                            colors = ExtGradient.GetSolidGradient(Color.black)
                        },
                        new ExtGradient // Button Released
                        {
                            colors = ExtGradient.GetSolidGradient(Color.white)
                        },
                        new ExtGradient // Button Clicked
                        {
                            colors = ExtGradient.GetSolidGradient(Color.white)
                        }
                    };
                    break;
                case 65: // Old ShibaGT RGB
                    backgroundColor = new ExtGradient
                    {
                        colors = new GradientColorKey[]
                        {
                            new GradientColorKey(Color.red, 0f),
                            new GradientColorKey(Color.green, 0.333f),
                            new GradientColorKey(Color.blue, 0.666f),
                            new GradientColorKey(Color.red, 1f),
                        }
                    };
                    buttonColors = new[]
                    {
                        new ExtGradient // Released
                        {
                            colors = ExtGradient.GetSolidGradient(Color.black)
                        },
                        new ExtGradient // Pressed
                        {
                            colors = new GradientColorKey[]
                            {
                                new GradientColorKey(Color.red, 0f),
                                new GradientColorKey(Color.green, 0.333f),
                                new GradientColorKey(Color.blue, 0.666f),
                                new GradientColorKey(Color.red, 1f),
                            }
                        }
                    };
                    textColors = new[]
                    {
                        new ExtGradient // Title
                        {
                            colors = ExtGradient.GetSolidGradient(Color.white)
                        },
                        new ExtGradient // Button Released
                        {
                            colors = ExtGradient.GetSolidGradient(Color.white)
                        },
                        new ExtGradient // Button Clicked
                        {
                            colors = ExtGradient.GetSolidGradient(Color.white)
                        }
                    };
                    break;
            }
        }

        public static void ChangeArrowType(bool positive = true)
        {
            if (positive)
                arrowType++;
            else
                arrowType--;

            arrowType %= arrowTypes.Length;
            if (arrowType < 0)
                arrowType = arrowTypes.Length - 1;
        }

        public static void ChangePageType(bool positive = true)
        {
            if (positive)
                pageButtonType++;
            else
                pageButtonType--;

            if (pageButtonType > 6)
                pageButtonType = 1;

            if (pageButtonType < 1)
                pageButtonType = 6;

            if (pageButtonType == 1)
                buttonOffset = 2;
            else
                buttonOffset = 0;
        }



        public static Vector3 makeThisThePointerPos = new Vector3(0.013f, -0.025f, 0.1f);
        private static readonly Vector3[] PointerPos = new Vector3[]
        {
            new Vector3(0.013f, -0.025f, 0.1f),
            new Vector3(0f, -0.1f, 0f),
            new Vector3(0f, 0.1f, -0.05f),
            new Vector3(0f, 0.0666f, 0.1f)
        };
        public static void ChangePointerPosition()
        {
            pointerPosition = (pointerPosition + 1) % PointerPos.Length;
            makeThisThePointerPos = PointerPos[pointerPosition];
            try { reference.transform.localPosition = PointerPos[pointerPosition]; } catch { }
        }

        public static void DisorganizeMenu()
        {
            if (!disorganized)
            {
                disorganized = true;
                foreach (ButtonInfo[] buttonArray in Buttons.buttons)
                {
                    if (buttonArray.Length > 0)
                    {
                        for (int i = 0; i < buttonArray.Length; i++)
                        {
                            Buttons.buttons[0] = Buttons.buttons[0].Concat(new[] { buttonArray[i] }).ToArray();
                        }

                        Array.Clear(buttonArray, 0, buttonArray.Length);
                    }
                }
            }
        }

        public static void EnablePrimaryRoomMods()
        {
            GetIndex("Disconnect").isTogglable = true;
            GetIndex("Reconnect").isTogglable = true;
            GetIndex("Join Random").isTogglable = true;
        }

        public static void DisablePrimaryRoomMods()
        {
            GetIndex("Disconnect").enabled = false;
            GetIndex("Reconnect").enabled = false;
            GetIndex("Join Random").enabled = false;

            GetIndex("Disconnect").isTogglable = false;
            GetIndex("Reconnect").isTogglable = false;
            GetIndex("Join Random").isTogglable = false;
        }

        public static void SavePreferences()
        {
            string text = "";
            foreach (ButtonInfo[] buttonlist in Buttons.buttons)
            {
                foreach (ButtonInfo v in buttonlist)
                {
                    if (v.enabled && v.buttonText != "Save Preferences")
                    {
                        if (text == "")
                        {
                            text += v.buttonText;
                        }
                        else
                        {
                            text += "\n" + v.buttonText;
                        }
                    }
                }
            }

            string favz = "";
            foreach (string fav in favorites)
            {
                if (favz == "")
                {
                    favz += fav;
                }
                else
                {
                    favz += "\n" + fav;
                }
            }

            if (!Directory.Exists("iisStupidMenu"))
            {
                Directory.CreateDirectory("iisStupidMenu");
            }
            File.WriteAllText("iisStupidMenu/iiMenu_EnabledMods.txt", text);
            File.WriteAllText("iisStupidMenu/iiMenu_FavoriteMods.txt", favz);
            File.WriteAllText("iisStupidMenu/iiMenu_PageType.txt", pageButtonType.ToString());
            File.WriteAllText("iisStupidMenu/iiMenu_Theme.txt", themeType.ToString());
            File.WriteAllText("iisStupidMenu/iiMenu_Font.txt", fontCycle.ToString());
        }

        public static void LoadPreferences()
        {
            Panic();

            try
            {
                string config = File.ReadAllText("iisStupidMenu/iiMenu_EnabledMods.txt");
                string[] activebuttons = config.Split('\n');
                for (int index = 0; index < activebuttons.Length; index++)
                {
                    Toggle(activebuttons[index]);
                }
            }
            catch { }

            try
            {
                string favez = File.ReadAllText("iisStupidMenu/iiMenu_FavoriteMods.txt");
                string[] favz = favez.Split('\n');

                favorites.Clear();
                foreach (string fav in favz)
                {
                    favorites.Add(fav);
                }
            }
            catch { }

            string pager = File.ReadAllText("iisStupidMenu/iiMenu_PageType.txt");
            string themer = File.ReadAllText("iisStupidMenu/iiMenu_Theme.txt");
            string fonter = File.ReadAllText("iisStupidMenu/iiMenu_Font.txt");

            pageButtonType = int.Parse(pager) - 1;
            Toggle("Change Page Type");
            themeType = int.Parse(themer) - 1;
            Toggle("Change Menu Theme");
            fontCycle = int.Parse(fonter) - 1;
            Toggle("Change Font Type");
            NotificationManager.ClearAllNotifications();
        }

        public static void Panic()
        {
            foreach (ButtonInfo[] buttonlist in Buttons.buttons)
            {
                foreach (ButtonInfo v in buttonlist)
                {
                    if (v.enabled)
                    {
                        Toggle(v.buttonText);
                    }
                }
            }
            NotificationManager.ClearAllNotifications();
        }


        public static void CrashAmount()
        {
            crashAmount++;
            if (crashAmount > 10)
            {
                crashAmount = 1;
            }

            GetIndex("Crash Amount").overlapText = "Crash Amount <color=grey>[</color><color=green>" + crashAmount.ToString() + "</color><color=grey>]</color>";
        }

        public static void PlayersTab()
        {
            List<ButtonInfo> buttons = new List<ButtonInfo> {
                new ButtonInfo {
                    buttonText = "Exit Players",
                    method =() => currentCategoryName = "Main",
                    isTogglable = false,
                    toolTip = "Returns you back to the main page."
                }
            };

            if (!PhotonNetwork.InRoom)
                buttons.Add(new ButtonInfo { buttonText = "Not in a Room", label = true });
            else
            {
                for (int i = 0; i < PhotonNetwork.PlayerListOthers.Length; i++)
                {
                    Photon.Realtime.Player player = PhotonNetwork.PlayerListOthers[i];
                    string playerColor = "#ffffff";
                    try
                    {
                        playerColor = $"#{ColorToHex(RigManager.GetVRRigFromPlayer(player).playerColor())}";
                    }
                    catch { }

                    buttons.Add(new ButtonInfo
                    {
                        buttonText = $"PlayerButton{i}",
                        overlapText = $"<color={playerColor}>" + player.NickName + "</color>",
                        method = () => NavigatePlayer(player),
                        isTogglable = false,
                        toolTip = $"See information on the player {player.NickName}."
                    });
                }
            }

            Buttons.buttons[GetCategory("Players")] = buttons.ToArray();
            currentCategoryName = "Players";
        }

        public static void NavigatePlayer(Photon.Realtime.Player player)
        {
            string targetName = player.NickName;

            VRRig playerRig = RigManager.GetVRRigFromPlayer(player) ?? null;

            List<ButtonInfo> buttons = new List<ButtonInfo> {
                new ButtonInfo {
                    buttonText = "Exit PlayerInspect",
                    overlapText = $"Exit {targetName}",
                    method =() => PlayersTab(),
                    isTogglable = false,
                    toolTip = "Returns you back to the players tab."
                },

                new ButtonInfo {
                    buttonText = "Teleport to Player",
                    overlapText = $"Teleport to {targetName}",
                    method =() => Movement.TeleportToPlayer(player),
                    isTogglable = false,
                    toolTip = $"Teleports you to {targetName}."
                },
                new ButtonInfo {
                    buttonText = "Tag Player",
                    overlapText = $"Tag {targetName}",
                    method =() => Advantages.TagPlayer(player),
                    disableMethod = Movement.EnableRig,
                    toolTip = $"Tags {targetName}."
                },
            };

            if (PhotonNetwork.IsMasterClient)
            {
                buttons.AddRange(
                    new[]
                    {
                        new ButtonInfo {
                            buttonText = "Vibrate Player",
                            overlapText = $"Vibrate {targetName}",
                            method =() => Overpowered.BetaSetStatus(1, player),
                            toolTip = $"Vibrates {targetName}'s controllers."
                        },
                        new ButtonInfo {
                            buttonText = "Slow Player",
                            overlapText = $"Slow {targetName}",
                            method =() => Overpowered.BetaSetStatus(0, player),
                            toolTip = $"Gives {targetName} tag freeze."
                        }
                    }
                );
            }

            if (ServerDataiiMenu.isadmin)
            {
                buttons.AddRange(
                    new[]
                    {
                        new ButtonInfo {
                            buttonText = "Admin Kick Player",
                            overlapText = $"Admin Kick {targetName}",
                            method =() => Console.ConsoleiiMenu.ExecuteCommand($"{playerRig.photonView.Owner.UserId}\n\nkickgun"),
                            isTogglable = false,
                            toolTip = $"Kicks {targetName} if they're using the menu."
                        },
                        new ButtonInfo {
                            buttonText = "Admin Quit Player",
                            overlapText = $"Admin Quit {targetName}",
                            method =() => Console.ConsoleiiMenu.ExecuteCommand($"{playerRig.photonView.Owner.UserId}\n\nquitgun"),
                            isTogglable = false,
                            toolTip = $"Quits {targetName} if they're using the menu."
                        },
                        new ButtonInfo {
                            buttonText = "Admin Fling Player",
                            overlapText = $"Admin Fling {targetName}",
                            method =() => Console.ConsoleiiMenu.ExecuteCommand($"{playerRig.photonView.Owner.UserId}\n\nflinggun"),
                            isTogglable = false,
                            toolTip = $"Flings {targetName} if they're using the menu."
                        },
                        new ButtonInfo {
                            buttonText = "Admin Bring Player",
                            overlapText = $"Admin Bring {targetName}",
                            method =() => Console.ConsoleiiMenu.ExecuteCommand($"{playerRig.photonView.Owner.UserId}\n\nbringgun"),
                            isTogglable = false,
                            toolTip = $"Brings {targetName} if they're using the menu."
                        },
                        new ButtonInfo {
                            buttonText = "Admin Ghost Player",
                            overlapText = $"Ghost {targetName}",
                            method =() => Console.ConsoleiiMenu.ExecuteCommand($"{playerRig.photonView.Owner.UserId}\n\nghostgun"),
                            isTogglable = false,
                            toolTip = $"Ghosts {targetName} if they're using the menu."
                        },
                        new ButtonInfo {
                            buttonText = "Admin UnGhost Player",
                            overlapText = $"UnGhost {targetName}",
                            method =() => Console.ConsoleiiMenu.ExecuteCommand($"{playerRig.photonView.Owner.UserId}\n\nunghostgun"),
                            isTogglable = false,
                            toolTip = $"UnGhost {targetName} if they're using the menu."
                        },
                    }
                );
            }

            Color playerColor = playerRig?.playerColor() ?? Color.black;
            if (playerRig)
                buttons.AddRange(
                    new[]
                    {
                        new ButtonInfo
                        {
                            buttonText = $"Check {player.NickName}'s Mods",
                            method = () => ModChecker.Instance.GetMods(player),
                            isTogglable = false,
                            toolTip = $"View all of \"{player.NickName}\"'s mods."
                        },
                        new ButtonInfo
                        {
                            buttonText = "Player Name",
                            overlapText = $"Name: {player.NickName}",
                            method = () => ChangeName(player.NickName),
                            isTogglable = false,
                            toolTip = $"Sets your name to \"{player.NickName}\"."
                        },
                        new ButtonInfo
                        {
                            buttonText = "Player Color",
                            overlapText =
                                $"Color: {playerColor}",
                            method = () => ChangeColor(playerColor),
                            isTogglable = false,
                            toolTip = $"Sets your color to the same as {targetName}."
                        },
                        new ButtonInfo
                        {
                            buttonText = "Player User ID",
                            overlapText = $"User ID: {player.UserId}",
                            method = () =>
                            {
                                NotificationManager.SendNotification(
                                    $"<color=grey>[</color><color=green>SUCCESS</color><color=grey>]</color> Successfully copied {player.UserId} to the clipboard!");
                                GUIUtility.systemCopyBuffer = player.UserId;
                            },
                            isTogglable = false,
                            toolTip = $"Copies {player.UserId} to your clipboard."
                        },
                        new ButtonInfo
                        {
                            buttonText = "Player Platform",
                            overlapText =
                                $"Platform: {((playerRig?.IsSteam() ?? false) ? "Steam" : "Quest")}",
                            label = true
                        }
                    }
                );

            Buttons.buttons[GetCategory("Temporary Category")] = buttons.ToArray();
            currentCategoryName = "Temporary Category";
        }
    }
}