import decimal
import string
import os
import math
from datetime import datetime
from os import path
import requests
from bs4 import BeautifulSoup


menu = [['g', 'Web Search'], ['p', 'Parse page'], ['s', 'Save']]
fileList = []
dicy = {}
retDict = {}
# The ** operator will convert the names arguments to a python dicitionary
def getpage(**kwargs):
    baseurl = "https://www.memoryexpress.com/Search/Products?"
    for k, v in kwargs.items():
        baseurl += f'{k}={v}&'

    print("\nWeb Request : ", baseurl)
    getResponse = requests.get(baseurl)
    print("Request status code: ", getResponse.status_code)

    if getResponse.status_code == 200:  # ok
        return getResponse.text
    else:
        return None


# it accepts the textual representation of the webpage (I beleive, the text of the get resposnse)
# returns a dictionary
def parsePage(webResponse):
    global myList
    bs = BeautifulSoup(webResponse, features='html.parser') #a new instance with the webresponse - the html text version
    print(bs.find('title').string, end="") #prints the text inside the title tag of the page

    # divColl = bs.find_all("div", attrs={"class":"c-shca-icon-item__summary-list"})
    divColl = bs.find_all("div", class_ = "c-shca-icon-item__summary-list")

    spanColl = []
    # each is a result object that can be used to invoke findAll() again
    for item in divColl:
        spanColl.append(item.findNext("span"))

    innerColl = []
    for i in spanColl:
        innerColl.append(i.string)

    priceList = []
    rtupleList = []

    # # A dictionary which has a tuple of min_range, max_range as it's key
    # # The value will be the list of all prices found in the range.
    myDict = {}
    for ic in innerColl:
        strippedItem = ic.strip(' \r\n')
        if strippedItem.startswith('$'):
            priceVal = float(strippedItem.strip(' $,\r\n'))#converts everything into a float
            priceList.append(priceVal) # a list with all the values in it
            minVal = float(math.floor(priceVal / 10) * 10)
            maxVal = float(math.ceil(priceVal/ 10) * 10)
            tuppy = tuple([minVal, maxVal]) #stores into a tuple of min and max range multiples of 10
            rtupleList.append(tuppy)

    # if the range tuple is not the dictionary, add it
    # if already present just add the value
    for tup in rtupleList:
        newList = []
        for priceVal in priceList:
            if priceVal >= tup[0] and priceVal < tup[1]:
                newList.append(priceVal)
                myDict[tup] = newList

    print(f", Found [{len(rtupleList)}] prices")
    for item in myDict:
        print(f"Price Range :{item} : {myDict[item]}")

    return myDict


# Save() helps in returning the filename saved,
# accepting a dictionary, and filename to attempt the save.
def save(myDict, myFileName):
    helo = ''
    # for writing to the specified file
    openFile = open(f"{myFileName}", 'w', encoding='utf-8-sig')
    currDTime = datetime.now().isoformat()
    with openFile as f:
        f.write(f"{myFileName} saved\n")
        f.write(f"Price information as of : {currDTime}\n\n")
        #sorted contatins args - any iterable, key and reverese = true for descending order
        for ky in sorted(myDict.keys(), key=lambda x:x[0]):
            f.write(str(ky) + "\n")

    # for reading from the specified file
    openFile = open(f"{myFileName}", 'r', encoding='utf-8-sig')
    with openFile as f:
        fileContent = f.readlines()


def Menu():
    print("Menu: ")
    for item in menu:
        print('{:4}'.format(item[0]), ': ', item[1])
    user_input = input("Selection: ")
    ValFunc(user_input)


def ValFunc(userinput):
    global dicy
    global myWebResponse
    global retDict
    dicy = {}
    if len(userinput) <= 0 or userinput.strip() == '' or str.isnumeric(userinput) or len(userinput) > 1 or (
            not userinput.isalpha()):
        print("Invalid Entry")
        Menu()
    else:
        match (userinput):
            case ('g'):
                print("Selection: g")
                search_input = input("Search : ")
                page_size_input = input("PageSize 40/[80]/120 : ")
                sort_input = input("Sort - Relevance/Price/PriceDesc/[Manufacturer] : ")

                if str.strip(page_size_input) == "":
                    page_size_input = 40
                if str.strip(sort_input) == "":
                    sort_input = "Manufacturer"

                dicy = {
                    "Search": search_input,
                    "PageSize": page_size_input,
                    "Sort": sort_input
                }
                myWebResponse = getpage(**dicy)  # returns the html as text of the page
                Menu()

            case ('p'):
                print("Selection: p")
                retDict = parsePage(myWebResponse)
                Menu()
            case ('s'):
                print("Selection: s")
                if len(retDict) == 0 or retDict is None:
                    print("Invalid selection")
                    Menu()
                else:
                    newFile = input("Please input filename: ")
                    split_tup = path.splitext(newFile)  # converts it into a tuple with the file name and its extension
                    myFileName = split_tup[0]
                    myFileExt = split_tup[1]

                    counter = 0
                    while path.exists(newFile) and counter < 99:  # checks if the file texts in the directory or not
                        counter += 1
                        newFile = myFileName + f"_{counter}" + myFileExt

                    save(retDict, newFile)
                    print(newFile, " saved")
                    Menu()
            case _:
                print("invalid selection, try again!")
                Menu()


if __name__ == '__main__':
    Menu()


# different ways of rounding using math.ceil or math.floor for the nearest integer
# print(round(18.7, -1)) #20
# print(math.ceil(18.7/10)*10) #20
# print(math.floor(19.2/10)*10) #10
# this is just for the accumulated count of the values in the dictionary
# sSum = 0
# for item in myDict:
#     print(f"Price Range :{item} : {myDict[item]}")
#     sSum+=len(myDict[item])
# print(sSum)


#
# from pathlib import Path
# print(Path.cwd())
# nList = os.listdir("C:\\Users\ASUS\\OneDrive - NAIT\\CMPE2850 - Winter 2023") # if nothing is specified, the default will be the current directory
# print(nList)
# print((lambda x:x+1)(2))'
