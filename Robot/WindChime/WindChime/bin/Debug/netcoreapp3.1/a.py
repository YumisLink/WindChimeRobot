from bs4 import BeautifulSoup
from urllib import request
import urllib.request
import urllib.error  # 指定URL,获取网页数据
import urllib


def getData(baseurl):
    # 解析数据
    headers = {
        'User-Agent': 'Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/84.0.4147.89 Safari/537.36 SLBrowser/7.0.0.6241 SLBChan/103'
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
    ans = ""
    for i in range(1, len(s) - 1):
        if s[i] == '>' and s[i - 1] == '\"':
            flag = 1
        elif flag == 1:
            if s[i] == '<' and s[i + 1] == '/':
                return ans
            else:
                ans += s[i]


def get_rating(name):
    baseurl = "http://codeforces.com/profile/" + name
    data = getData(baseurl)
    bs = BeautifulSoup(data, "html.parser")
    # print(bs)
    temp = bs.select(
        '#pageContent > div:nth-child(3) > div.userbox > div.info > ul > li:nth-child(1) > span.user-cyan')
    s = str(temp)
    # print(s)
    rating = myre(s)
    if rating is None:
        return "None"
    # print(rating)
    else:
        return rating


if __name__ == '__main__':
    name = input()
    # name = "BingHui"
    rating = get_rating(name)
    print(rating)
#   get_rating返回str类型
#   用户名存在则返回分数，不存在返回“None"
