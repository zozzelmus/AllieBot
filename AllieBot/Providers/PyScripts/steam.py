import json
import requests 
from bs4 import BeautifulSoup

response = requests.get(give_url)
soup = BeautifulSoup(response.text, features='html.parser')

steam_name = soup.find('span', {'class': 'actual_persona_name'}).text

real_name = soup.find('bdi').text

steam_level = soup.find('span', {'class': 'friendPlayerLevelNum'}).text

steam_img = soup.find('div', {'class': 'playerAvatarAutoSizeInner'}).findAll('img')[1].attrs['src']

steam_status = soup.find('div', {'class': 'profile_in_game_header'}).text


values = {
    'steam name': steam_name,
    'real name': real_name,
    'steam level':steam_level,
    'steam img': steam_img,
    'steam status': steam_status
}

retVal = json.dumps(values)