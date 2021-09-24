from bs4 import BeautifulSoup
from urllib import request
import urllib.request
import urllib.error  # 指定URL,获取网页数据
import urllib
from urllib.request import quote
import re
import sys


def getData(baseurl):
    # 解析数据
    headers = {
        'User-Agent': 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/93.0.4577.63 Safari/537.36'
    }
    req = urllib.request.Request(baseurl, headers=headers)
    try:
        response = urllib.request.urlopen(req)
        data = response.read().decode("utf-8")
        # print(data)
        return data
    except urllib.error.URLError as e:
        if hasattr(e, "code"):
            print(e.code)
        if hasattr(e, "reason"):
            print(e.reason)
        return 'Error'


def myre(s):
    flag = 0
    ret = []
    ans = ""
    tag = False
    det = False
    start = False
    reload = False
    xb = 0
    for i in range(1, len(s) - 1):
        if s[i] == '<':
            if reload:
                ret.append(ans)
                ans = ""
                reload = False
            det = False
            tag = True
        if start and det:
            if tag:
                tag = False
            reload = True
            ans += s[i]
        if det:
            start = True
        start = True
        if s[i] == '>':
            det = True
    return ret


def ChineseLeave(k):
    ans = []
    for i in k:
        if re.search(r'^[A-Z]{0,1}[0-9]*-[0-9]*', i) != None:
            continue
        if re.search(r'^JT[0-9]*-[0-9]*', i) != None:
            continue
        if re.search(r'^专精', i) != None:
            continue
        if (i == "罕见"):
            continue
        if (i == "小概率"):
            continue
        if (i == "概率"):
            continue
        if (i == "\n"):
            continue
        if (i == "、"):
            continue
        if (i == "额外物资"):
            continue
        if (i == "固定掉落"):
            continue
        if (i == "空中威胁"):
            continue
        if (i == "概率掉落"):
            continue
        ans.append(i)
    return ans


def Delete(args):
    ret = [args[1]]
    l = len(args)
    print(l)
    if (l == 2):
        return "N"
    k = int(l/3)
    for i in range(0, k):
        ret.append(args[2+i*3])
        ret.append(args[3+i*3])
    return ret


def GetString(name, skill, type):
    name = quote(name)
    # print(name)
    baseurl = "https://wiki.biligame.com/arknights/" + name
    skill += 1
    type += 2
    # k = GetString(skill)
    data = getData(baseurl)
    bs = BeautifulSoup(data, "html.parser")
    # print(baseurl)
    # print(bs)
    temp = bs.select(
        '#mw-content-text > div > div.resp-tabs > div > div:nth-child(3) > div:nth-child(3) > table:nth-child(' + str(skill) + ') > tbody')
    s = str(temp)
    # print(s)
    k = myre(s)
    # return ChineseLeave(k)
    return Delete(ChineseLeave(k))


if __name__ == '__main__':
    name = input()
    skill = input()
    ans = GetString(name, int(skill), 1)
    for i in ans:
        print(i)
