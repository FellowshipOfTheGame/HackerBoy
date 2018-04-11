import numpy as np
import argparse

global TYPO_CHANCE
global SIDE_CHANCE
global SWAP_CHANCE
global direction
global keyboard
global text

TYPO_CHANCE = 0.07
SIDE_CHANCE = 0.70
SWAP_CHANCE = 0.06

keyboard = (
	(u'q', u'w', u'e', u'r', u't', u'y', u'u', u'i', u'o', u'p'),
	(u'a', u's', u'd', u'f', u'g', u'h', u'j', u'k', u'l', u'k'),
	(u'z', u'x', u'c', u'v', u'b', u'n', u'n', u'm', u'm', u' ')
)

Dir = {"up":1, "down":2, "left":3, "right":4}

def GbariateText(text):

	int i;
	for i in range(len(text)):
		if rand()/RAND_MAXF <= TYPO_CHANCE:
			text[i] = Typo(text[i])
		if rand()/RAND_MAXF <= SWAP_CHANCE:
			Swap(text, i)
	pass

def Typo(text):
	pass

def RandomizeChar(text, dir):
	int i, j;

	FindChar(text, &i, &j);

	
	if direction == Dir['up']:
		if i > 0:
			return keyboard[i-1][j]

	elif direction == Dir['down']:
		if i < NROW-1:
			return keyboard[i+1][j]

	elif direction == Dir['right']:
		if j > 0:
			return keyboard[i][j+1]

	elif direction == Dir['left']:
		if j < NCOL-1:
			return keyboard[i][j-1]

	else:
		fprintf(stderr, "Error\n");
		return -1;

	return text;

def Swap(text, index):
	aux = text[index];
	text[index] = text[index+1];
	text[index+1] = aux;
	return

def FindChar(char, i, j):
	for i in range(0, keyboard.shape[0])
		for j in range(0, keyboard.shape[1])
			if keyboard[i][j] == char
				return (i, j)


ap = argparse.ArgumentParser()
ap.add_argument("-p", "--shape-predictor", required=True,
	help="path to facial landmark predictor")
ap.add_argument("-i", "--image", required=True,
	help="path to input image")


text = text.lower()

GbariateText(text, size);

print(text);
return 0	