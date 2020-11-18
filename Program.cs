﻿using System;

namespace krumbs
{
    class Program
    {

        static void Main(string[] args)
        {
            // 3840, 1600
            int screenW = 3840;
            int screenH = 1600;

            // Console.WriteLine("Hello World!");
            // new Launch().launch();
            var krumbs = new Krumbs();
            var process = krumbs.launch("WoW - 1");
            krumbs.removeBorder(process);
            //krumbs.position(process, 0, 0, 1920, 1080);
            krumbs.position(process, 0, 0, screenW / 4 * 3, screenH);

            for (int i = 0; i < 4; i++) {
                var potato= krumbs.launch($"WoW - {i+1}");
                // border needs to be removed before position
                krumbs.removeBorder(potato);
                krumbs.position(potato, screenW / 4 * 3, screenH / 4 * i, screenW / 4, screenH / 4);
            }
            // var potato1 = krumbs.launch("WoW - 2");
            // krumbs.position(potato1, screenW / 4 * 3, screenH / 4 * 0, screenW / 4, screenH / 4);
            // var potato2 = krumbs.launch("WoW - 3");
            // krumbs.position(potato2, screenW / 4 * 3, screenH / 4 * 1, screenW / 4, screenH / 4);
            // var potato3 = krumbs.launch("WoW - 4");
            // krumbs.position(potato3, screenW / 4 * 3, screenH / 4 * 2, screenW / 4, screenH / 4);
            // var potato4 = krumbs.launch("WoW - 5");
            // krumbs.position(potato4, screenW / 4 * 3, screenH / 4 * 3, screenW / 4, screenH / 4);
        }
    }
}
