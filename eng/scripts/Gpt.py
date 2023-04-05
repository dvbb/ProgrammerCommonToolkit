import openai
# pip install --upgrade openai

api_key = "<your-api-key>"

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
            text = input('你：')
            if text == 'quit':
                break

            d = {"role":"user","content":text}
            messages.append(d)

            text = askChatGPT(messages)
            d = {"role":"assistant","content":text}
            print('ChatGPT：'+text+'\n')
            messages.append(d)
        except:
            messages.pop()
            print('ChatGPT：error\n')
main()
