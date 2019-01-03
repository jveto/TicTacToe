using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace TicTacToe.Models{
    public class Numbers{
        public int[] Boxes{get; set;} = {0, 0, 0, 0, 0, 0, 0, 0, 0};
        public int Box1{get; set;} = 0;
        public int Box2{get; set;} = 0;
        public int Box3{get; set;} = 0;
        public int Box4{get; set;} = 0;
        public int Box5{get; set;} = 0;
        public int Box6{get; set;} = 0;
        public int Box7{get; set;} = 0;
        public int Box8{get; set;} = 0;
        public int Box9{get; set;} = 0;
    }
}