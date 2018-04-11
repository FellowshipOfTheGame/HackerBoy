#include <stdio.h>
#include <iostream>
#include <stdlib.h>
#include <string.h>
#include <wchar.h>
#include <time.h>
#include <omp.h>

using namespace std;

#define SIZE 3*1024*1024
#define RAND_MAXF ((float) RAND_MAX)
#define TYPO_CHANCE 0.07f
#define SIDE_CHANCE 0.70f
#define SWAP_CHANCE 0.06f

#define NROW 3
#define NCOL 10

char keyboard[3][10] = {
	{'q', 'w', 'e', 'r', 't', 'y', 'u', 'i', 'o', 'p'},
	{'a', 's', 'd', 'f', 'g', 'h', 'j', 'k', 'l', 'k'},
	{'z', 'x', 'c', 'v', 'b', 'n', 'n', 'm', 'm', ' '}
};

enum Direction { UP, DOWN, LEFT, RIGHT };

void GbariateText(char *text, int n);
char Typo(char text);
char RandomizeChar(char text, enum Direction dir);
void Swap(char *text, int index);
void FindChar(char text, int *i, int *j);
void ToLower(char *text, int n);

int main(int argc, char *argv[]){

	char *text = (char *) malloc(sizeof(char)*SIZE);
	int size;
	
	const wchar_t *str = L"ãéóáö";
	const char *str1 = u8"ãéóáö";
	const char32_t *str2 = U"ãéóáö";

	fwide(stdout, 1);
	wprintf(L"ãéóáö %s\n", str);
	wcout << L"ãéóáö \u0444 " << str << endl;
	printf("ãéóáö %s\n", str1);
	wcout << u8"ãéóáö \u0444 " << str1 << endl;
	printf("ãéóáö %ls\n", str2);
	wcout << U"ãéóáö \u0444 " << str2 << endl;
	return 0;

	srand(time(NULL)*clock());
	text[0] = '\0';

	if(argc == 1){
		printf("Enter text: ");
		scanf("%[^\n]s", text);
	} else for(int i = 1; i < argc; i++){
		text = strcat(text, argv[i]);
		text = strcat(text, " ");
	}

	size = strlen(text);

	ToLower(text, size);
	GbariateText(text, size);

	printf("%s", text);

	free(text);
	return 0;
}

void GbariateText(char *text, int n){

	int i;
	for(i = 0; i < n; i++){
		if(rand()/RAND_MAXF <= TYPO_CHANCE) text[i] = Typo(text[i]);
		if(rand()/RAND_MAXF <= SWAP_CHANCE) Swap(text, i);
	}
}

char Typo(char c){
	// Horizontal-typo 
	if(rand()/RAND_MAXF <= SIDE_CHANCE){

		if(rand()%2) return RandomizeChar(c, RIGHT);
		else return RandomizeChar(c, LEFT);

	// Vertical-typo
	} else {

		if(rand()%2) return RandomizeChar(c, UP);
		else return RandomizeChar(c, DOWN);
	}
}

char RandomizeChar(char text, enum Direction dir){

	int i, j;

	FindChar(text, &i, &j);

	switch(dir){
	case UP:
		if(i > 0) return keyboard[i-1][j];
		break;

	case DOWN:
		if(i < NROW-1) return keyboard[i+1][j];
		break;

	case LEFT:
		if(j > 0) return keyboard[i][j-1];
		break;

	case RIGHT:
		if(j < NCOL-1) return keyboard[i][j+1];
		break;

	default:
		fwprintf(stderr, L"Errõó“ÅGØGX¢çbr\n");
		return -1;
	}

	return text;
}

void Swap(char *text, int index){
	char aux = text[index];
	text[index] = text[index+1];
	text[index+1] = aux;
}

void FindChar(char text, int *row, int *col){

	#pragma omp parallel for
	for(int i = 0; i < NROW; i++){
		for(int j = 0; j < NCOL; j++){
			if(keyboard[i][j] == text){
				*row = i;
				*col = j;
				break;
			}
		}
	}
}

void ToLower(char *text, int n){
	for(int i = 0; i < n; i++){
		if(text[i] >= 'A' && text[i] <= 'Z')
			text[i] += 'a';
	}
}





