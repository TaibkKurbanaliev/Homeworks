# Before optimization
1) Instantiate/Destroy bullets и Enemies
2) Draw calls и map camera не настроена.

<img width="1201" height="262" alt="BeforeOptimizeEnemy" src="https://github.com/user-attachments/assets/a81685ae-cbf6-456d-9f2e-228fe360a120" />
<img width="1196" height="162" alt="BeforeOptimizeBullet" src="https://github.com/user-attachments/assets/6e587d48-7706-4a1a-9da3-8e2e7234ec95" />
<img width="733" height="200" alt="BeforeOptimazeDraw" src="https://github.com/user-attachments/assets/f3829cb9-8dec-4c3e-ae19-805f7b87d719" />
<img width="888" height="490" alt="image" src="https://github.com/user-attachments/assets/9133cd61-2049-4759-a36e-72f8494fc555" />

# After optimization
1) Инициализация object pool для bullets и enemies при старте сцены
2) Включение static batching для бочек и dynamic для врагов, также отключение теней, сглаживания, post-processing и тд, для map camera, что снизило максимальное и минимальное время отрисовки

<img width="1602" height="160" alt="AfterOptimizeEnemy" src="https://github.com/user-attachments/assets/2cfacffc-8043-4d83-95b3-2ba7f4a070ce" />
<img width="732" height="208" alt="AfterOptimizeDraw" src="https://github.com/user-attachments/assets/5a3ef009-defe-4b62-8a66-77bd0cd4b301" />
<img width="1607" height="180" alt="AfterOptimizeBullet" src="https://github.com/user-attachments/assets/8c705b1a-72cc-48cc-8386-27ab4dad14a1" />
<img width="1173" height="484" alt="image" src="https://github.com/user-attachments/assets/af52c99e-c8aa-4897-a79a-6f6f0fe3b0f2" />

# Итоговая таблица

| Метрика             | До   | После   |
| ------------------- | ---- | ------- |
| Draw Calls          | 421  | **291** |
| GC Alloc / frame    | 2.5KB | **1.5KB** |
