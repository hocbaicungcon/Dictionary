import time
import re

file = open("en.txt", "r")
sum = 0
for i in file:
	value = re.findall(r'\d+', i)
	sum = sum + int(value[0])
		
print sum
#output sum = 145 376 051