using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using WMPLib;

namespace BattleShip.Implementations
{
    public static class SoundEffects
    {

        public static  void BattleBgmPlayer(WindowsMediaPlayer bgm)
        {
            bgm.URL = @"Sounds\BGM.wav";
            bgm.settings.setMode("loop", true);
        }

        public static void HitShipSoundPlayer()
        {
            var player = new SoundPlayer(@"Sounds\HitShip.wav");
            player.Play();
        }

        public static void HitWaterSoundPlayer()
        {
            var player = new SoundPlayer(@"Sounds\HitWater.wav");
            player.Play();
        }

        public static void SunkenSoundPlayer()
        {
            var player = new SoundPlayer(@"Sounds\Sunken.wav");
            player.Play();
        }

        public static void SetShipSoundPlayer()
        {
            SoundPlayer player = new SoundPlayer(@"Sounds\SetShip.wav");
            player.Play();
        }

        public static void WinnerSoundPlayer(WindowsMediaPlayer winnerBgm)
        {
            winnerBgm.URL = @"Sounds\GameOverWinner.wav";
            winnerBgm.settings.setMode("loop", true);
        }

        public static void LoserSoundPlayer()
        {
            SoundPlayer player = new SoundPlayer(@"Sounds\GameOverLoser.wav");
            player.Play();
        }

        public static void TypeSoundPlayer(WindowsMediaPlayer bgm)
        {
            bgm.URL = @"Sounds\TypeSound.wav";
            bgm.settings.setMode("loop", true);
        }



    }
}
