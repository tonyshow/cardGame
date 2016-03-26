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
dir='./in'			#Դ�ļ��洢Ŀ¼
outPath='./out/' 	#Ŀ���ļ��洢Ŀ¼
csPath = 'Script'	#Ŀ���ļ��洢Ŀ¼
txtPath = 'configs'#Ŀ���ļ��洢Ŀ¼
classAfter='Detail'
dataTypeLine = 1    #����������
indexLine = 2		#������������
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
def createCS(outPath,name,nrows,ncols,fileKeys,indexNotes,dataTypeKeys,vNotesColumn): 
	outPath= outPath.replace('out','out/'+csPath);
	print'createCS -- outPath = %s '%outPath
	#cs����
	thisClassName = name+classAfter
	FileCS= open(outPath,'wb+')
	#��İ�������
	times = time.localtime();
	year = times.tm_year
	tm_mon  = times.tm_mon
	tm_mday  = times.tm_mday
	tm_hour  = times.tm_hour
	tm_min  = times.tm_min
	tm_sec  = times.tm_sec
	allTime = '//Create Time :'+ str(year)+'-'+str(tm_mon)+'-'+str(tm_mday)+' '+str(tm_hour)+':'+str(tm_min)+':'+str(tm_sec); 
	FileCS.write(allTime+'\t')
	FileCS.write(createOkStr("using UnityEngine;"))
	FileCS.write(createOkStr("using System.Collections;"))
	FileCS.write(createOkStr(""))  
	
	FileCS.write(createOkStr("public class "+thisClassName))
	FileCS.write(createOkStr("{"))
 
	#�������ֵ� 	 
	fristLineIndexList = indexNotes.split("#"); 	#��һ�е�ע��
	secondLineIndexList = fileKeys.split("#");
	threeLineIndexList = dataTypeKeys.split("#");
		 
	#�������
	index_temp = 0
	for variable in secondLineIndexList:		
		if( index_temp<vNotesColumn):			
			dataStr = '	private ' + threeLineIndexList[index_temp] +' _'+ variable + ';'
			index_temp = index_temp+1
			FileCS.write(createOkStr(dataStr))
		
	FileCS.write(createOkStr(""))
	#������ֵ����
	FileCS.write(createOkStr("	public void configData(string[] vRows)"))
	FileCS.write(createOkStr("	{"));
	
	index_temp = 0
	for variable in secondLineIndexList:
		if( index_temp<vNotesColumn):
			typeS = threeLineIndexList[index_temp]  
			#int���ʹ���
			tempS = "int" 
			iB = int( typeS  == tempS )		
			if( iB >0 ):
				FileCS.write(createOkStr('		//'+typeS))		
				itemStr = '		_'+variable + '=' + typeS + '.Parse(vRows['+str(index_temp)+']);'
				FileCS.write(createOkStr(itemStr))
			
			#int[]���ʹ���
			tempS = "int[]" 
			iB = int( typeS  == tempS )		
			if( iB >0 ):
				FileCS.write(createOkStr('		//'+typeS))
				TempStr = variable+'Temp'
				dataStr = '		string[] '+TempStr+ ' = vRows['+str(index_temp)+'].Split(new char[] { \'#\' });'
				FileCS.write(createOkStr(dataStr))
				dataStr1 = '		'+ '_'+variable+' = new int['+TempStr+'.Length];'
				FileCS.write(createOkStr(dataStr1))
				dataStr2 = '		for(int i = 0 ; i < '+TempStr+'.Length ; ++i)'  
				FileCS.write(createOkStr(dataStr2))
				FileCS.write(createOkStr('		{'))
				dataStr3 = '			_'+variable+'[i] = int.Parse('+TempStr+'[i]);'
				FileCS.write(createOkStr(dataStr3)) 
				FileCS.write(createOkStr('		}'))
				
			#float���ʹ���	
			tempS = "float" 
			iB = int( typeS  == tempS )		
			if( iB >0 ): 
				FileCS.write(createOkStr('		//'+typeS))
				itemStr = '		_'+variable + '=' + typeS + '.Parse(vRows['+str(index_temp)+']);'
				FileCS.write(createOkStr(itemStr))
			
			#float[]���ʹ���
			tempS = "float[]" 
			iB = int( typeS  == tempS )		
			if( iB >0 ):
				FileCS.write(createOkStr('		//'+typeS))
				TempStr = variable+'Temp'
				dataStr = '		'+typeS+ ' '+TempStr+ ' = vRows['+str(index_temp)+'].Split(new char[] { \'#\' });'
				FileCS.write(createOkStr(dataStr))
				dataStr1 = '		'+ '_'+variable+' = new float['+TempStr+'.Length];'
				FileCS.write(createOkStr(dataStr1))
				dataStr2 = '		for(int i = 0 ; i < '+TempStr+'.Length ; ++i)'  
				FileCS.write(createOkStr(dataStr2))
				FileCS.write(createOkStr('		{'))
				dataStr3 = '			_'+variable+'[i] = float.Parse('+TempStr+'[i]);'
				FileCS.write(createOkStr(dataStr3)) 
				FileCS.write(createOkStr('		}'))
				
				
				
			#string���ʹ���
			tempS = "string" 
			iB = int( typeS  == tempS )		
			if( iB >0 ):
				FileCS.write(createOkStr('		//'+typeS))		
				itemStr = '		_'+variable + '=' + 'vRows['+str(index_temp)+'];'
				FileCS.write(createOkStr(itemStr))
				
			#string[]���ʹ���
			tempS = "string[]" 
			iB = int( typeS  == tempS )		
			if( iB >0 ): 	
				FileCS.write(createOkStr('		//'+typeS))
				TempStr = variable+'Temp'
				dataStr = '		'+typeS+ ' '+TempStr+ ' = vRows['+str(index_temp)+'].Split(new char[] { \'#\' });'
				FileCS.write(createOkStr(dataStr))
				dataStr1 = '		'+ '_'+variable+' = new int['+TempStr+'.Length];'
				FileCS.write(createOkStr(dataStr1))
				dataStr2 = '		for(int i = 0 ; i < '+TempStr+'.Length ; ++i)'  
				FileCS.write(createOkStr(dataStr2))
				FileCS.write(createOkStr('		{'))
				dataStr3 = '			_'+variable+'[i] = int.Parse('+TempStr+'[i]);'
				FileCS.write(createOkStr(dataStr3)) 
				FileCS.write(createOkStr('		}'))

				
			#Vector2���ʹ���
			tempS = "Vector2" 
			iB = int( typeS  == tempS )		
			if( iB >0 ):  
				FileCS.write(createOkStr('		//'+typeS))
				dataStr3 = '		string[] xy = vRows['+str(index_temp)+'].Split(new char[] { \'&\' });'
				FileCS.write(createOkStr(dataStr3)) 
				FileCS.write(createOkStr("		if(xy.Length==2)"))
				FileCS.write(createOkStr("		{"))
				dataStr4_x = '			float x = float.Parse(xy[0]);'
				FileCS.write(createOkStr(dataStr4_x)) 
				dataStr4_y = '			float y = float.Parse(xy[1]);'
				FileCS.write(createOkStr(dataStr4_y)) 
				
				dataStr5 = '			_'+variable+' = new '+tempS+'(x,y);'
				FileCS.write(createOkStr(dataStr5)) 
				FileCS.write(createOkStr("		}"))
			#Vector2[]���ʹ���
			tempS = "Vector2[]" 
			iB = int( typeS  == tempS )		
			if( iB >0 ): 
				FileCS.write(createOkStr('		//'+typeS))
				TempStr = variable+'Temp'
				dataStr = '		'+ 'string[] '+TempStr+ ' = vRows['+str(index_temp)+'].Split(new char[] { \'#\' });'
				FileCS.write(createOkStr(dataStr))
				FileCS.write(createOkStr("		if("+TempStr+".Length>0)"))
				dataStr1 = '			'+ '_'+variable+' = new Vector2['+TempStr+'.Length];'
				FileCS.write(createOkStr(dataStr1))
				dataStr2 = '		for(int i = 0 ; i < '+TempStr+'.Length ; ++i)'  
				FileCS.write(createOkStr(dataStr2))
				FileCS.write(createOkStr('		{')) 
				dataStr3 = '			string[] xy = '+TempStr+'.Split(new char[] { \'&\' });'
				FileCS.write(createOkStr(dataStr3)) 
				
				FileCS.write(createOkStr("			if(xy.Length==2)"))
				FileCS.write(createOkStr("			{"))
				dataStr4_x = '				float x = float.Parse(xy[0]);'
				FileCS.write(createOkStr(dataStr4_x)) 
				dataStr4_y = '				float y = float.Parse(xy[1]);'
				FileCS.write(createOkStr(dataStr4_y)) 
				
				dataStr5 = '				_'+variable+'[i] = new Vector2(x,y);'
				FileCS.write(createOkStr(dataStr5)) 
				FileCS.write(createOkStr("			}"))			
				FileCS.write(createOkStr('		}'))		
				
			#Vector3���ʹ���
			tempS = "Vector3" 
			iB = int( typeS  == tempS )		
			if( iB >0 ):  
				FileCS.write(createOkStr('		//'+typeS))
				dataStr3 = '		string[] xyz =vRows['+str(index_temp)+'].Split(new char[] { \'&\' });'
				FileCS.write(createOkStr(dataStr3)) 
				
				FileCS.write(createOkStr("		if(xyz.Length==3)"))
				FileCS.write(createOkStr("		{"))
				dataStr4_x = '			float x = float.Parse(xyz[0]);'
				FileCS.write(createOkStr(dataStr4_x)) 
				dataStr4_y = '			float y = float.Parse(xyz[1]);'
				FileCS.write(createOkStr(dataStr4_y)) 
				dataStr4_z = '			float z = float.Parse(xyz[2]);'
				FileCS.write(createOkStr(dataStr4_z)) 
				
				dataStr5 = '			_'+variable+' = new '+tempS+'(x,y,z);'
				FileCS.write(createOkStr(dataStr5)) 
				FileCS.write(createOkStr("		}"))			
			
			#Vector3[]���ʹ���
			tempS = "Vector3[]" 
			iB = int( typeS  == tempS )		
			if( iB >0 ): 
				FileCS.write(createOkStr('		//'+typeS))
				TempStr = variable+'Temp' 
				dataStr = '		'+ 'string[] '+TempStr+ ' = vRows['+str(index_temp)+'].Split(new char[] { \'#\' });'
				FileCS.write(createOkStr(dataStr))
				FileCS.write(createOkStr("		if("+TempStr+".Length>0)"))
				dataStr1 = '			'+ '_'+variable+' = new Vector3['+TempStr+'.Length];'
				FileCS.write(createOkStr(dataStr1))
				dataStr2 = '		for(int i = 0 ; i < '+TempStr+'.Length ; ++i)'  
				FileCS.write(createOkStr(dataStr2))
				FileCS.write(createOkStr('		{')) 
				dataStr3 = '			string[] xyz = '+TempStr+'[i].Split(new char[] { \'&\' });'
				FileCS.write(createOkStr(dataStr3)) 
				
				FileCS.write(createOkStr("			if(xyz.Length==3)"))
				FileCS.write(createOkStr("			{"))
				dataStr4_x = '				float x = float.Parse(xyz[0]);'
				FileCS.write(createOkStr(dataStr4_x)) 
				dataStr4_y = '				float y = float.Parse(xyz[1]);'
				FileCS.write(createOkStr(dataStr4_y)) 
				dataStr4_z = '				float z = float.Parse(xyz[2]);'
				FileCS.write(createOkStr(dataStr4_z)) 
				
				dataStr5 = '				_'+variable+'[i] = new Vector3(x,y,z);'
				FileCS.write(createOkStr(dataStr5)) 
				FileCS.write(createOkStr("			}"))
				
				FileCS.write(createOkStr('		}'))			
				
			index_temp = index_temp + 1
			FileCS.write(createOkStr(""))
	FileCS.write(createOkStr("	}"))
	FileCS.write(createOkStr(""))
	
	#����ֶη���
	index_temp = 0
	for variable in secondLineIndexList:
		if( index_temp<vNotesColumn):
			typeS = threeLineIndexList[index_temp] 
			s = fristLineIndexList[index_temp];
			s.decode("UTF-8")
			strTemp = '	/*'+s+'*/'
			FileCS.write(createOkStr(strTemp)) 
			strTemp = '	public ' + typeS + ' '+ variable + '{ get { return _'+variable+';} }'
			FileCS.write(createOkStr(strTemp))
			index_temp = index_temp + 1	
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
	dataTypeKeys='';
	indexNotes='';
	sheetNumber = 0;
	notesColumn = 1000;#ע����
	for sheetName in wb.sheet_names(): 
		sheetNumber = sheetNumber + 1;
		if( sheetNumber == 1):  
			name = sheetName;
			fullTxtPath = outPath+'\\'+name+'.txt';
			fullTxtPath= fullTxtPath.replace('out','out/'+txtPath); 
			txtFile=open(fullTxtPath,'wb+')
			txtFile.read().decode("utf-8")
			sheet = wb.sheet_by_name(sheetName)
			ncols = sheet.ncols
			for rownum in range(sheet.nrows):
				v=""
				dataStr=""				
				for ncol in range( sheet.ncols ):										
					v1 = sheet.cell(rownum, ncol).value	
					#�ֶ�ע��
					if (int(rownum) == 0 ):
						if int(ncol)< int(sheet.ncols-1):							
							nameStrTemp = v1;
							returnValue = nameStrTemp.find('#');
							if(returnValue==0):
								if(notesColumn>int(ncol)):
									notesColumn = int(ncol);
							else:
								indexNotes = indexNotes + v1+'#';
						else:							
							nameStrTemp = v1;
							returnValue = nameStrTemp.find('#');
							if(returnValue==0):
								if(notesColumn>int(ncol)):
									notesColumn = int(ncol);
							else:
								indexNotes = indexNotes + v1;
					#�ֶ�����
					if (int(rownum) == dataTypeLine ):  						
						if (int(ncol) != notesColumn ):
							if int(ncol)< int(sheet.ncols-1): 
								dataTypeKeys = dataTypeKeys + v1 + '#'
							else:
								dataTypeKeys = dataTypeKeys + v1
					#�ֶ���
					if (int(rownum) == indexLine ):  
						if (int(ncol) != notesColumn ):
							if int(ncol)< int(sheet.ncols-1):
								fileKeys = fileKeys + v1+'#'; 
							else:
								fileKeys = fileKeys + v1; 
					if (int(notesColumn)>int(ncol)):
						if (type(v1) == float):     
							v1 = str(v1)
							v1 = re.sub('\.0*$', "", v1) 
						v1 = v1.rstrip();
						if(int(ncol) < notesColumn-1 and int(ncol) < int(sheet.ncols) - 1 ):
							v =  v + v1 +"	"
						else:
							v =  v + v1
				if (int(rownum) < (int(sheet.nrows)-1) ):
					dataStr = dataStr + v+ '\r\n'
				else:
					dataStr = dataStr + v
				if(rownum!=1):				
					txtFile.write(dataStr)  
			txtFile.close()
			createCS(outPath+'\\' +name+classAfter+'.cs',name,sheet.nrows,sheet.ncols,fileKeys,indexNotes,dataTypeKeys,notesColumn)	
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
	#�ļ��в���
	elif os.path.isdir(src):
		try: 
			outFolder_1 = src;
			outFolder_1 = outFolder_1.replace('in','out')
			outFolder_1 = outFolder_1.replace('\\','/')  
			
			csFolderPath  = outFolder_1;
			txtFolderPath = outFolder_1;
			csFolderPath   = csFolderPath.replace('out','out/'+csPath)
			txtFolderPath  = txtFolderPath.replace('out','out/'+txtPath) 
			os.mkdir( csFolderPath )
			os.mkdir( txtFolderPath )
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
	
	
	if  (os.path.exists(outPath + csPath)):
		sjdghg = ''
	else:
		os.mkdir( outPath + csPath ) 
		
	if  (os.path.exists(outPath + txtPath)):
		sjdghg = ''
	else:
		os.mkdir( outPath + txtPath )
	
	file_folder(dir);
main()