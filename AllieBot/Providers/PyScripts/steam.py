import requests 
from bs4 import BeautifulSoup

give_url = input('Enter a Steam profile URL: ')

response = requests.get(give_url)

soup = BeautifulSoup(response.text, features='html.parser')

#print(soup, type(soup), sep='\n') <- prints all info on steam account page

steam_name = soup.find('span', {'class': 'actual_persona_name'}).text
print(steam_name)

real_name = soup.find('bdi').text
print(real_name)

steam_level = soup.find('span', {'class': 'friendPlayerLevelNum'}).text
print(steam_level)

steam_img = soup.find('div', {'class': 'playerAvatarAutoSizeInner'})
print(steam_img.findAll('img')[1].attrs['src'])

steam_status = soup.find('div', {'class': 'profile_in_game_header'}).text
print(steam_status)
