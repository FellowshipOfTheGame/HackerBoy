using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Inventory : MonoBehaviour {

	public static readonly int TYPE = 1;
	public static readonly int ALPHA = 2;

	private List<Item> items;

	public Inventory(){
		items = new List<Item>();
	}

	public Inventory(int size){
		items = new List<Item>(size);
	}

	public void AddItem(Item item){
		items.Add(item);
	}

	public void UseItem(int index){
		if(items[index].type == ItemType.Consumable)
			(items[index] as Consumable).Use();
	}

	//  Multiple sorting modes?
	public void SortInventory(int sortType){
		if(sortType == TYPE){
			
			// Lambda sort
			items.Sort((a, b) => (
				(int) (a.type - b.type)
				)
			);

		} else if(sortType == ALPHA){
			
			// Lambda sort
			items.Sort((a, b) => (
				System.String.Compare(a.name, b.name, true)
				)
			);
		}
	}

	public static int TypeSort(Item i1, Item i2){
		return (int) (i1.type - i2.type);
	}

	public static int AlphabeticalSort(Item i1, Item i2){
		return System.String.Compare(i1.name, i2.name, true);
	}
}
