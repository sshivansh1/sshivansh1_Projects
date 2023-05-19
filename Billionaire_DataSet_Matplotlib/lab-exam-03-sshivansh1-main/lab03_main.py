from matplotlib import pyplot as plt
import csv
import numpy as np
import mplcursors

fig, ax = plt.subplots(1, 1, figsize=(16, 10), facecolor = "sandybrown")
def OpenFile():
    # open the file
    with open('The Top Billionaires.csv', encoding='utf-8-sig') as csvfile:
        reader = csv.DictReader(csvfile)
        print("Fieldnames: ", reader.fieldnames)

        myList = [dict(rec) for rec in reader]
        return myList


if __name__ == "__main__":
    sFile = OpenFile()
    nameSeq = [item["NAME"] for item in sFile]
    seq2018 = [item["2018"] for item in sFile]
    seq2019 = [item["2019"] for item in sFile]
    seq2020 = [item["2020"] for item in sFile]
    seq2021 = [item["2021"] for item in sFile]
    seq2022 = [item["2022"] for item in sFile]

    seq2022_list = [float(x) for x in seq2022]
    seq2021_list = [float(x) for x in seq2021]
    seq2020_list = [float(x) for x in seq2020]
    seq2019_list = [float(x) for x in seq2019]
    seq2018_list = [float(x) for x in seq2018]

    ax.plot(nameSeq, seq2022_list, marker = "o", label = "2022")
    ax.plot(nameSeq, seq2021_list, marker = "o", label = "2021")
    ax.plot(nameSeq, seq2020_list, marker = "o", label = "2020")
    ax.plot(nameSeq, seq2019_list, marker = "o", label = "2019")
    ax.plot(nameSeq, seq2018_list, marker = "o", label = "2018")

    ax.set_facecolor("linen")
    ax.legend(title="Years", fontsize=16)

    ax.set_xlabel("Billionaire names", fontsize = 20, labelpad=20)
    ax.set_ylabel("Income (in Billions)", fontsize = 20, labelpad=20)
    ax.set_title("The Top Billionaires", fontsize = 30, pad = 20)

    crs = mplcursors.cursor(ax, hover=True)

    crs.connect("add", lambda sel: sel.annotation.set_text('({})'.format(round(sel.target[1], 2))))

    fig.text(0.9, 0.15, 'Shivansh', fontsize=14, color='grey', ha='right', va='bottom', alpha=0.3)

    plt.show()