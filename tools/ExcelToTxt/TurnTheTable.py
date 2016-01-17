#coding:utf-8 
import time
import codecs
import sys
reload(sys)
sys.setdefaultencoding('utf-8')
import os
import glob
import xlrd
import re 
import codecs
path = os.getcwd() 
dir="./in"			#Դ�ļ��洢Ŀ¼
outPath="./out/" 	#Ŀ���ļ��洢Ŀ¼
classAfter='DataManager'
indexLine = 1		
def delete_file_folder(src): 
    if os.path.isfile(src):  
        try:  
            os.remove(src)  
        except:  
            pass 
    elif os.path.isdir(src):  
        for item in os.listdir(src):  
            itemsrc=os.path.join(src,item)  
            delete_file_folder(itemsrc)  
        try:  
            os.rmdir(src)  
        except:  
            pass 	
def removeFileInFirstDir(targetDir): 
	for file in os.listdir(targetDir): 
		targetFile = os.path.join(targetDir,  file)  
		if os.path.isfile(targetFile):
			os.remove(targetFile)
		else:
			delete_file_folder(targetFile)
			

def createOkStr(data): 	 
	return '\n'+data+ '\r'
 

#����.cs�ļ�
def createCS(outPath,name,nrows,ncols,fileKeys,indexNotes): 
	#cs����
	thisClassName = name+classAfter
	FileCS= open(outPath,'wb+')
	print 'create cs  file : %s '%(thisClassName+'.cs')
	#��İ�������
	times = time.localtime();
	year = times.tm_year
	tm_mon  = times.tm_mon
	tm_mday  = times.tm_mday
	tm_hour  = times.tm_hour
	tm_min  = times.tm_min
	tm_sec  = times.tm_sec
	allTime = '//Create Time :'+ str(year)+'-'+str(tm_mon)+'-'+str(tm_mday)+' '+str(tm_hour)+':'+str(tm_min)+':'+str(tm_sec); 
	FileCS.write(createOkStr(allTime))
	FileCS.write(createOkStr("using UnityEngine;"))
	FileCS.write(createOkStr("using System.Collections;"))
	FileCS.write(createOkStr("using System.Collections.Generic;"))
	FileCS.write(createOkStr("using UnityEngine.UI;"))
	FileCS.write(createOkStr("using DG.Tweening;"))
	FileCS.write(createOkStr("using UnityEngine;"))
	FileCS.write(createOkStr("public class "+thisClassName))
	FileCS.write(createOkStr("{"))
	
	#�������ֵ� 
	fileKeysList = fileKeys.split("&"); 
	indexNotesList = indexNotes.split("#"); 
	#FileCS.write(createOkStr("	/*��ʼ���������ֵ�*/"))	
	FileCS.write(createOkStr("	private static Dictionary<string, int> indexList = new Dictionary<string, int>()"))
	FileCS.write(createOkStr("	{"))
	
	no = 0;
	for grounds in fileKeysList:
		item = '';
		mIndexNum=0;
		for key in grounds.split("#"):
			mIndexNum=mIndexNum+1;
			if(mIndexNum%2==0):
				item=item+str(key)+'},'
			else:
				item='		{"'+item+key+'",'
		s = indexNotesList[no];
		s.decode("UTF-8")
		item = item + '		//' + s ;
		no=no+1;
		FileCS.write(createOkStr(item))
	FileCS.write(createOkStr("	};"))
	FileCS.write(createOkStr(""))
	
	
	#���в���
	FileCS.write(createOkStr("	public static "+thisClassName+" instance = null;")) 
	FileCS.write(createOkStr("	public static "+thisClassName+" getInstance()")) 
	FileCS.write(createOkStr("	{")) 
	FileCS.write(createOkStr("    	if (instance == null)"))  
	FileCS.write(createOkStr("    	{"))
	FileCS.write(createOkStr("     	   instance = new "+thisClassName+"();"))
	FileCS.write(createOkStr("    	}"))
	FileCS.write(createOkStr("		return instance;")) 
	FileCS.write(createOkStr("	}"))  
	FileCS.write(createOkStr(""))
	
	#������ȡ������
	#FileCS.write(createOkStr("	/*ͨ��������ȡ��ǰ�����ڵڼ���*/"))	
	FileCS.write(createOkStr("	public int getIndex(string keys){"))
	FileCS.write(createOkStr("		if(null==indexList[keys]){"))
	FileCS.write(createOkStr("			Debug.Error(\"no find keys: \"+keys);"))
	FileCS.write(createOkStr("			return 0;"))
	FileCS.write(createOkStr("		}"))
	
	FileCS.write(createOkStr("		return indexList[keys];"))
	FileCS.write(createOkStr("	}"))
	FileCS.write(createOkStr(""))
	
	#������ȡ������
	#FileCS.write(createOkStr("	/*��ñ������*/"))	
	FileCS.write(createOkStr("	public int getNrows(){")) 
	FileCS.write(createOkStr("		return "+str(nrows)+";"))
	FileCS.write(createOkStr("	}"))
	FileCS.write(createOkStr(""))
	
	#������ȡ������
	#FileCS.write(createOkStr("	/*��ñ������*/"))		
	FileCS.write(createOkStr("	public int getNcols(){"))
	FileCS.write(createOkStr("		return "+str(ncols)+";"))
	FileCS.write(createOkStr("	}")) 
	FileCS.write(createOkStr(""))
	
	FileCS.write(createOkStr("}")) 
	FileCS.close() 

#����txt�ļ�	
#paths 		ԭ�ļ�·��    		 in\Custom.xlsx
#name		�ļ�����			 Custom
#outPath	����ļ���·��      ./out/
def createTxt(paths,name,outPath): 
	print'\nread xlsx file : %s'%paths
	wb = xlrd.open_workbook(paths)
	fileKeys = '';
	indexNotes='';
	for sheetName in wb.sheet_names(): 
		if(sheetName == 'Sheet1'):    
			txtFile=open(outPath+'\\'+name+'.txt','wb+')
			txtFile.read().decode("utf-8")
			print'create txt file : %s'%name+'.txt'
			sheet = wb.sheet_by_name(sheetName)
			ncols = sheet.ncols
			for rownum in range(sheet.nrows):
				v=""
				dataStr=""				
				for ncol in range( sheet.ncols ):										
					v1 = sheet.cell(rownum, ncol).value
					if (int(rownum) == 0 ):
						if int(ncol)< int(sheet.ncols-1):
							indexNotes = indexNotes + v1+'#';
						else:
							indexNotes = indexNotes + v1;
					if (int(rownum) == indexLine ):  
						if int(ncol)< int(sheet.ncols-1):
							fileKeys = fileKeys + v1+'#'+str(ncol)+'&';
						else:
							fileKeys = fileKeys + v1+'#'+str(ncol);
					if (type(v1) == float):     
						v1 = str(v1)
						v1 = re.sub('\.0*$', "", v1) 
					v1 = v1.rstrip()
					v =  v +v1+ '	'
				dataStr = '\n'+dataStr + v+ '\r'
				txtFile.write(dataStr)  
			txtFile.close()
			createCS(outPath+'\\' +name+classAfter+'.cs',name,sheet.nrows,sheet.ncols,fileKeys,indexNotes)	
def file_folder(src):  
	if os.path.isfile(src):  
		try:
			#Ϊ�ļ�
			outFilePath = src
			outFilePath = outFilePath.replace('in','out')
			outFilePath = outFilePath.replace('\\','/') 
			outFilePath = outFilePath
			src = src.replace('\\','/')  
			listFolder = outFilePath.split("/")
			length = len(listFolder)-1;
			fileName_xlsx = listFolder[length]; 
			fileName = (fileName_xlsx.split("."))[0]; 
			outFilePath = outFilePath.replace(fileName_xlsx,'')  
			createTxt(src,fileName,outFilePath)
		except:
			pass
	elif os.path.isdir(src):
		try: 
			outFolder_1 = src;
			outFolder_1 = outFolder_1.replace('in','out')
			outFolder_1 = outFolder_1.replace('\\','/')  
			os.mkdir( outFolder_1 ) 
		except:
			pass 
		for item in os.listdir(src):
			itemsrc=os.path.join(src,item)
			file_folder(itemsrc)			

def main(): 
	if  (os.path.exists(dir)):
		sjdghg = ''
	else:
		os.mkdir( dir )
		
	if  (os.path.exists(outPath)):
		sjdghg = ''
	else:
		os.mkdir( outPath ) 
	removeFileInFirstDir(outPath)
	
	file_folder(dir);
main()