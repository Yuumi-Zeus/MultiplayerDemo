﻿using YOGA.Modules.架构模式.Common;

namespace YOGA.Modules.架构模式.MVC
{
    public class Controller
    {
        public Model model;

        public Controller(Model model) => this.model = model;
    }
}
