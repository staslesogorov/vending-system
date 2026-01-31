from selenium import webdriver
from selenium.webdriver.common.by import By
import time

driver = webdriver.Chrome()
driver.get("http://localhost:3000")
time.sleep(1)

driver.find_elements(By.CSS_SELECTOR, "input.border.border-gray-200")[0].send_keys('test')
time.sleep(1)
driver.find_elements(By.CSS_SELECTOR, "input.border.border-gray-200")[1].send_keys('123')
time.sleep(1)
driver.find_element(By.CSS_SELECTOR, "button.mt-5.p-2.w-full.bg-blue-400").click()
time.sleep(1)

driver.find_element(By.XPATH, "//span[contains(text(), 'Мониторинг ТА')]").click()
time.sleep(1)

if "monitoring" in driver.current_url:
    print(True)
else:
    print(False)
    
driver.quit()