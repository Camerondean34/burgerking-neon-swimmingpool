﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurgerPoolGame.Scenes
{
    public interface IScene
    {
        void Draw(float pSeconds);
        void Update(float pSeconds);
    }
}
