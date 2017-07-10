import threading

def run_thread(action):
    thread = threading.Thread(target = action)
    thread.start()
