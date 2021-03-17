# Import from packages
import os
import argparse
import logging
import win32com.client

# Import from modules
from MyCls import initialise_app, finalise_app, handle_exception

# Initialise project
CURR_DIR, CURR_FILE = os.path.split(__file__)
PROJ_NAME = CURR_FILE.split('.')[0]

# Get command line arguments
my_arg_parser = argparse.ArgumentParser(description=f"{PROJ_NAME}")
#my_arg_parser.add_argument("arg1", help="Text1")
my_arg_parser.add_argument("--log", help="DEBUG to enter debug mode")
args = my_arg_parser.parse_args()

# Initialise app
initialise_app(PROJ_NAME, args.log)
logger = logging.getLogger("my_logger")

# # Get environment variables
# env_var1 = os.getenv("env_var1")
# env_var2 = os.getenv("env_var2")
# if env_var1 == None or env_var2 == None:
#     handle_exception("Missing environment variables!")

##################################################
# Variables
##################################################


##################################################
# Functions
##################################################


##################################################
# Main
##################################################
# Extract tasks from txt
with open("data/calendar.txt", 'r') as reader:
    tasks = reader.readlines()
logger.info(f"{len(tasks)} tasks extracted")

# Init Outlook Calendar folder
app = win32com.client.Dispatch("Outlook.Application")
my_namespace = app.GetNamespace("MAPI")
folder = my_namespace.GetDefaultFolder(9).Folders("[Import]") # 9 for Calendar folder

# Clear Outlook Calendar folder
del_count = 0
for i in range (folder.Items.Count, 0, -1):
    folder.Items(i).Delete()
    del_count += 1
logger.info(f"{del_count} appointments deleted")

# Create appointments
create_count = 0
for task in tasks:
    task_name, start, end, cat = task.split(';')
    appt_itm = folder.Items.Add(1) # 1 for AppointmentItem object
    appt_itm.Subject = task_name
    appt_itm.Start = start
    appt_itm.End = end
    appt_itm.Categories = cat
    appt_itm.AllDayEvent = True
    appt_itm.ReminderSet = False
    appt_itm.Save()
    create_count += 1
logger.info(f"{create_count} appointments created")

finalise_app()