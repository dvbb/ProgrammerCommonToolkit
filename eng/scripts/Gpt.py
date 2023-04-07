import openai
import os
# pip install --upgrade openai

api_key = "<your-api-key>"
api_key = os.getenv('OpenAIApiKey')

openai.api_key = api_key

def askChatGPT(messages):
    MODEL = "gpt-3.5-turbo"
    response = openai.ChatCompletion.create(
        model=MODEL,
        messages = messages,
        temperature=1)
    return response['choices'][0]['message']['content']

def main():
    messages = [{"role": "user","content":""}]
    while 1:
        try:
            text = input('[You]：')
            if text == 'quit':
                break

            d = {"role":"user","content":text}
            messages.append(d)

            text = askChatGPT(messages)
            d = {"role":"assistant","content":text}
            print('\n[GPT]：'+text+'\n')
            print('--------------------------------\n')
            messages.append(d)
        except:
            messages.pop()
            print('ChatGPT：error\n')
main()
