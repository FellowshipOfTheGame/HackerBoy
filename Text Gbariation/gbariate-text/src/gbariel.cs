using System;
using System.Text;

class Gbariation {

	private enum Direction { UP, DOWN, LEFT, RIGHT }

	private static char[,] keyboard;
	private static Random rand;
	private static readonly int NROW = 3;
	private static readonly int NCOL = 10;
	private static readonly float TYPO_CHANCE = 0.07f;
	private static readonly float SIDE_CHANCE = 0.70f;
	private static readonly float SWAP_CHANCE = 0.06f;
	private static readonly float DOT_CHANCE  = 0.06f;

	static int Main(string[] args) {
		
		bool forever = true;
		rand = new Random();

		char[,] _keyboard = new char[3, 10]{
			{'q', 'w', 'e', 'r', 't', 'y', 'u', 'i', 'o', 'p'},
			{'a', 's', 'd', 'f', 'g', 'h', 'j', 'k', 'l', 'k'},
			{'z', 'x', 'c', 'v', 'b', 'n', 'n', 'm', 'm', ' '}
		};

		keyboard = _keyboard;
		string text = null;

		Console.WriteLine("Keep entering text\n\n");

		try {

			while(forever){
				if(args.Length == 0){
					text = Console.ReadLine();
				} else {
					forever = false;
					for(int i = 0; i < args.Length; i++)
						text += args[i] + " ";
				}

				text = text.ToLower();
				text = GbariateText(new StringBuilder(text));

				Console.WriteLine(text);
			}
		} catch (Exception e){}

		return 0;
	}

	private static string GbariateText(StringBuilder text){

		int i;
		for(i = 0; i < text.Length; i++){
			if(text[i] == ' ' && rand.NextDouble() <= DOT_CHANCE) text[i] = '.';
			else if(rand.NextDouble() <= TYPO_CHANCE) text[i] = Typo(text[i]);
			if(rand.NextDouble() <= SWAP_CHANCE) text = new StringBuilder(Swap(text, i));
		}

		return text.ToString();
	}

	private static char Typo(char c){
		// Horizontal-typo 
		if(rand.NextDouble() <= SIDE_CHANCE){

			if(rand.NextDouble() <= 0.5) return RandomizeChar(c, Direction.RIGHT);
			else return RandomizeChar(c, Direction.LEFT);

		// Vertical-typo
		} else {

			if(rand.NextDouble() <= 0.5) return RandomizeChar(c, Direction.UP);
			else return RandomizeChar(c, Direction.DOWN);
		}
	}

	private static char RandomizeChar(char text, Direction dir){

		int i = 0, j = 0;

		FindChar(text, ref i, ref j);

		switch(dir){
		case Direction.UP:
			if(i > 0) return keyboard[i-1, j];
			break;

		case Direction.DOWN:
			if(i < NROW-1) return keyboard[i+1, j];
			break;

		case Direction.LEFT:
			if(j > 0) return keyboard[i, j-1];
			break;

		case Direction.RIGHT:
			if(j < NCOL-1) return keyboard[i, j+1];
			break;

		default:
			Console.WriteLine("Error");
			return ' ';
		}

		return text;
	}

	private static string Swap(StringBuilder text, int index){
		
		try {
			char aux = text[index];
			text[index] = text[index+1];
			text[index+1] = aux;
			
		} catch (Exception e) {
			return text.ToString();
		}

		return text.ToString();
	}

	private static void FindChar(char text, ref int row, ref int col){
		for(int i = 0; i < NROW; i++){
			for(int j = 0; j < NCOL; j++){
				if(keyboard[i, j] == text){
					row = i;
					col = j;
					return;
				}
			}
		}
	}
}
