# BinarySearchTree-HashTable-Heap-SortingAlgorithm
Data Structures -- Binnary Search Tree - HashTable - Heap - Sorting Algorithm - project

                            PROJECT DETAİL
---for example---
Station Object (Park name, Empty Parking, Tandem bicycle, Normal bicycle)
String[] Parks = { “İnciraltı, 28, 2, 10”, “Sahilevleri, 8, 1, 11“, “Doğal Yaşam Parkı, 17, 1, 6”, “Bostanlı İskele, 7, 0, 5“ };

1)a)Create the Station Objects by separating the strings in the parks array into their fields, and write the code that places them in the binary search tree named Stations Tree according to the Station Name. When adding each Station object to the tree, add 1 to 10 random numbers of random Customers (Customer ID, rental time) to the node in a List type data structure (Assume that a total of 20 customers are registered in the system ID: 1 - ID: 20). Update the number of empty parks and bicycles. Figure 1 shows the tree with 3 nodes.

![image](https://user-images.githubusercontent.com/44711182/110497881-e748e380-8107-11eb-95ae-5eead1c1a3be.png)

b) Write the method that finds the depth of the tree. Write the method that lists all the information in the tree (including the number of customers rented by the list and the information in the List) on the screen.

c) For a Customer ID given on the keyboard, go through all the nodes of the tree and write the method that lists the time and time of the bike rented from which stations by looking at the information in the lists.

d) Rental process: Write a method that adds the relevant information (ID + random time) to the List by performing a normal bicycle rental transaction of the person whose Customer Number (ID) is given from a named station. The number of BP will also increase by 1.

2) a) Write the code that places the Station Information specified in Question 1 into a Hash Table according to the Park Name. You can use the Stations Array. (You don't need to add customers in this question)

b) Write the code that updates the Hash Table by adding 5 normal bicycles to all stations with more than 5 Free Parking Places.

3)a) Design a Heap Data Structure (class). You can use an array or a List or a Vector to keep its elements in the infrastructure. Code and run it.

b) Just write the code that places nodes in a max Heap based on Normal Bike numbers.

c) Take the three Stations with at most Normal Bicycles from Heap and write the code that lists the relevant station information.

4) a) (Simple Sorting) and watch the change of variables in Debug by selecting a sorting algorithm.

b) Encode (Advanced Sorting) by choosing a sorting algorithm.

c) Calculate the time complexity of both methods.
