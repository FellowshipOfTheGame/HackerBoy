using System;
using System.Text;

class AdinoText {

	private static readonly float ADINOE_CHANCE = 0.1f;

	public static Random rand = new Random();

	static int Main(string[] args) {
		
		string text = null;

		try {
			if(args.Length == 0){
				text = Console.ReadLine();
			} else {
				for(int i = 0; i < args.Length; i++)
					text += args[i] + " ";
			}

			text = text.ToLower();
			text = Adinotext(text);

			Console.WriteLine(text);

		} catch (Exception){

		}

		return 0;
	}

	private static string Adinotext(string text){

		int i;
		string[] strs = text.Split(' ');

		for(i = 0; i < strs.Length; i++){
			if(rand.NextDouble() <= ADINOE_CHANCE){
				strs[i] = "adino" + strs[i] + " ";
			} else strs[i] += " ";
		}

		return string.Join("", strs);
	}
}