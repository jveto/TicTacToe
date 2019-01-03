using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TicTacToe.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace TicTacToe.Controllers
{
    public static class SessionExtensions{
            public static void SetObjectAsJson(this ISession session, string key, object value){
                session.SetString(key, JsonConvert.SerializeObject(value));
            }
            public static T GetObjectFromJson<T>(this ISession session, string key){
                string value = session.GetString(key);
                return value == null ? default(T) : JsonConvert.DeserializeObject<T>(value);
            }
        }
    public class HomeController : Controller
    {
        
        //List<int> Numbers = new List<int>();
        Numbers Squares = new Numbers();

        [HttpGet("")]
        public IActionResult Index()
        {

            System.Console.WriteLine("Index@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@");
            
            List<int> Options = new List<int>();

            Numbers Retrieve = HttpContext.Session.GetObjectFromJson<Numbers>("numbers");
            if(Retrieve == null){
                HttpContext.Session.SetObjectAsJson("numbers", Squares);
                Retrieve = HttpContext.Session.GetObjectFromJson<Numbers>("numbers");
            }
            // Loop through the session Boxes, check which ones are still playable, and add them to list for players to choose
            for(int i = 0; i < 9; i++){
                if(Retrieve.Boxes[i] == 0){
                    Options.Add(i+1);
                }
            }
            

            ViewBag.Options = Options;
            ViewBag.MarkedBoxes = Retrieve.Boxes;
            int? RetrievePlayer = HttpContext.Session.GetInt32("player");
            if(RetrievePlayer == null){
                HttpContext.Session.SetInt32("player", 0);
            }
            

            // Set the game state
            String RetrieveGameState = HttpContext.Session.GetString("GameState");
            if(RetrieveGameState == null){
                HttpContext.Session.SetString("GameState", "Active");
                RetrieveGameState = HttpContext.Session.GetString("GameState");
            }
            // Need an option to check if all moves have been finished
            if(Options.Count == 0){
                System.Console.WriteLine("^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^");
                HttpContext.Session.SetString("GameState", "CatsGame");
            }
            ViewBag.player = HttpContext.Session.GetInt32("player"); 
            ViewBag.GameState = HttpContext.Session.GetString("GameState");;
            return View();
        }

        [HttpPost("move")]
        public IActionResult Move(int Box){
            System.Console.WriteLine("Here is the number selected");
            System.Console.WriteLine(Box);

            // Check what int the player is: 1 = Player O, 0 = Player X
            int? Player = HttpContext.Session.GetInt32("player");
            Numbers numbers = HttpContext.Session.GetObjectFromJson<Numbers>("numbers");

            // Change Box value to fit into the Numbers Array 
            Box -= 1;

            // Depending on which player, change the array value to the designated player number.  Player 0 = 1, Player 1 = 2
            if((int)Player == 0){
                numbers.Boxes[Box] = 1;
            }
            else{
                numbers.Boxes[Box] = 2;
            }

            // Check to see if current player wins off this move

            //Bottom Row
            if(new[] {numbers.Boxes[0], numbers.Boxes[1], numbers.Boxes[2]}.All(x => x == 1)){
                // End game, Player 1 won
                System.Console.WriteLine("Player X got the bottom row");
                HttpContext.Session.SetString("GameState", "P1Win");
            }
            else if(new[] {numbers.Boxes[0], numbers.Boxes[1], numbers.Boxes[2]}.All(x => x == 2)){
                // End game, Player 2 won
                System.Console.WriteLine("Player O got the bottom row");
                HttpContext.Session.SetString("GameState", "P2Win");
            }

            //Middle Row
            if(new[] {numbers.Boxes[3], numbers.Boxes[4], numbers.Boxes[5]}.All(x => x == 1)){
                System.Console.WriteLine("Player X got the middle row");
                HttpContext.Session.SetString("GameState", "P1Win");
            }
            else if(new[] {numbers.Boxes[3], numbers.Boxes[4], numbers.Boxes[5]}.All(x => x == 2)){
                System.Console.WriteLine("Player O got the middle row");
                HttpContext.Session.SetString("GameState", "P2Win");
            }

            //Top Row
            if(new[] {numbers.Boxes[6], numbers.Boxes[7], numbers.Boxes[8]}.All(x => x == 1)){
                HttpContext.Session.SetString("GameState", "P1Win");
            }
            else if(new[] {numbers.Boxes[6], numbers.Boxes[7], numbers.Boxes[8]}.All(x => x == 2)){
                HttpContext.Session.SetString("GameState", "P2Win");
            }

            //Left Column
            if(new[] {numbers.Boxes[0], numbers.Boxes[3], numbers.Boxes[6]}.All(x => x == 1)){
                HttpContext.Session.SetString("GameState", "P1Win");
            }
            else if(new[] {numbers.Boxes[0], numbers.Boxes[3], numbers.Boxes[6]}.All(x => x == 2)){
                HttpContext.Session.SetString("GameState", "P2Win");
            }

            //Middle Column
            if(new[] {numbers.Boxes[1], numbers.Boxes[4], numbers.Boxes[7]}.All(x => x == 1)){
                HttpContext.Session.SetString("GameState", "P1Win");
            }
            else if(new[] {numbers.Boxes[1], numbers.Boxes[4], numbers.Boxes[7]}.All(x => x == 2)){
                HttpContext.Session.SetString("GameState", "P2Win");
            }

            //Right Column
            if(new[] {numbers.Boxes[2], numbers.Boxes[5], numbers.Boxes[8]}.All(x => x == 1)){
                HttpContext.Session.SetString("GameState", "P1Win");
            }
            else if(new[] {numbers.Boxes[2], numbers.Boxes[5], numbers.Boxes[8]}.All(x => x == 2)){
                HttpContext.Session.SetString("GameState", "P2Win");
            }

            //Top left corner
            if(new[] {numbers.Boxes[6], numbers.Boxes[4], numbers.Boxes[2]}.All(x => x == 1)){
                HttpContext.Session.SetString("GameState", "P1Win");
            }
            else if(new[] {numbers.Boxes[6], numbers.Boxes[4], numbers.Boxes[2]}.All(x => x == 2)){
                HttpContext.Session.SetString("GameState", "P2Win");
            }

            //Bottom left corner
            if(new[] {numbers.Boxes[0], numbers.Boxes[4], numbers.Boxes[8]}.All(x => x == 1)){
                HttpContext.Session.SetString("GameState", "P1Win");
            }
            else if(new[] {numbers.Boxes[0], numbers.Boxes[4], numbers.Boxes[8]}.All(x => x == 2)){
                HttpContext.Session.SetString("GameState", "P2Win");
            }


            System.Console.WriteLine("!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
            System.Console.WriteLine(numbers.Box1);
            System.Console.WriteLine(numbers.Box2);
            HttpContext.Session.SetObjectAsJson("numbers", numbers);


            // Change the current player
            if((int)Player == 0){
                HttpContext.Session.SetInt32("player", 1);
            }
            else{
                HttpContext.Session.SetInt32("player", 0);
            }

            return RedirectToAction("");
        }

        [HttpPost("new")]
        public IActionResult New(){
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }

    }
}
