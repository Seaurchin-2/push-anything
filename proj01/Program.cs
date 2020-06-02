/*
    I have ever written comment by English,
    so this code is my first one.
    Actually, I used google translator for all comment...
    Sorry for my terrible English skills
    and spaghetti c# code.
    Thank you for reading this!
*/

using System;
using System.Collections.Generic;
using System.Threading;
using System.Linq;

class Program {
    public const int input_lction_x = 35; // x coordinates of reading user's input
    public const int input_lction_y = 15; // y coordinates of reading user's input
    public const int wndow_wide = 90; // console window wide
    public const int wndow_height = 40; // console window height

    static void Main(string[] args){
        string game_str = "underground"; // a word used in gameplay
        string usr_input = ""; // variable inputted by user
        int correct_count = 0; // variable for counting how many letter corrected
        
        try{
            GameEvent gEvent = new GameEvent( // basic window setting
                wndow_wide, wndow_height, game_str, input_lction_x, input_lction_y
            );
            while(true){
                // move cursor to setted location and read user's input
                Console.SetCursorPosition(input_lction_x+11, input_lction_y);
                usr_input = Console.ReadLine();
                gEvent.Wrong_Alert(false);

                // save index bundle of matching letter with user's input
                List<int> input_ltr_index = gEvent.Find_Match_str(usr_input, game_str);
                /* <debugging code>
                foreach(var sample in input_ltr_index){
                    Console.WriteLine("d) Added Index: " + Convert.ToString(sample));
                }*/

                // replace '_' letter to correct letter
                for(int count3=0; count3!=input_ltr_index.Count; count3++){
                    gEvent.Match_str_Update(
                        ((int)(wndow_wide-game_str.Length*2)/2)+(2*input_ltr_index[count3])+1, 12,
                        game_str[input_ltr_index[count3]]
                    );
                    correct_count++;
                    // <debugging code>
                    // Thread.Sleep(500);
                }

                // start when all the letters are corrected
                if(correct_count == game_str.Length){
                    gEvent.Celebration();
                }
            }
        }
    
        catch(Exception e){ // start when code has an exception\
            // show the info about caused exception
            Console.WriteLine(
                e.Message + "\n" + e.Source + "\n" + e.StackTrace
            );
            Console.WriteLine(e.Data.ToString());
        }
        // start when the catch command ends
        finally {
            // move cursor to down and exit
            Console.SetCursorPosition(0, 60);
            Console.Write("Press any key to continue . . .");
            Console.ReadKey();
        }
    }
}

// class for finding matching letter or replacing letter in gameplay
class GameEvent {
    public GameEvent( // constructor for basic window setting
        int wndow_wide, int wndow_height, string game_str, int input_lction_x, int input_lction_y
    ){
        string print_str = "[ Enter alphabet to find full Letter! ]";
        Console.Clear();
        Console.SetWindowSize(wndow_wide, wndow_height);
        Console.SetCursorPosition(
            (int)((wndow_wide-print_str.Length)/2), 7
        );
        Console.Write(print_str);
        Console.SetCursorPosition(
            (int)(wndow_wide-game_str.Length*2)/2, 12
        );
        for(int count1=0; count1!=game_str.Length; count1++){
            Console.Write("_ ");
        }
        Console.SetCursorPosition(input_lction_x, input_lction_y);
        Console.Write("> Answer : ");
    }

    // method for finding matching letter in word on gameplay
    public List<int> Find_Match_str(string input_letter, string game_str){
        List<char> game_char = new List<char>(game_str.ToLower());
        List<int> result = new List<int>();
        int count2 = 0;
        foreach (var ltr in game_char){
            if(ltr.ToString() == input_letter)
                result.Add(count2);
            count2++;
        }
        if(!result.Any()){
            this.Wrong_Alert(true);
        }
        return result;
    }

    //
    public void Match_str_Update(int lction_x, int lction_y, char change_ltr){
        Console.SetCursorPosition(lction_x, lction_y);
        Console.Write("\b" + change_ltr);
    }

    // method for warning for no-matching letter
    public void Wrong_Alert(bool Alrt_or_Clr){
        string alert_msg = "!= There's no such a letter you inputted =!";
        if(Alrt_or_Clr){
            Console.SetCursorPosition((Program.wndow_wide-alert_msg.Length)/2, 10);
            Console.Write(alert_msg);
        }
        else {
            Console.SetCursorPosition((Program.wndow_wide-alert_msg.Length)/2, 10);
            for(int count4=0; count4!=alert_msg.Length; count4++)
                Console.Write(" ");
        }
    }

    // method for congratulating user that corrected all the letters
    public void Celebration(){
        Console.Clear();
        for(int count5=0; count5!=Program.wndow_wide-1; count5++){
            Console.SetCursorPosition(count5, 0);
            Console.Write("-");
            Console.SetCursorPosition(count5, Program.wndow_height-1);
            Console.Write("-");
        }
        Console.SetCursorPosition(10, (int)(Program.wndow_height/2));
        Console.WriteLine("Congratulation! You win!");
    }
}